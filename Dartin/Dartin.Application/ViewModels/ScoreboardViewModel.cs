using Caliburn.Micro;
using Dartin.Abstracts;
using Dartin.Managers;
using Dartin.Models;
using Dartin.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Parser = Dartin.Managers.Parser;
using System.Diagnostics;

namespace Dartin.ViewModels
{
    class ScoreboardViewModel : Screen, IViewModel
    {
        private Leg _leg;
        private int _player1LegScore;
        private int _player2LegScore;
        private string _input;
        private string _currentLeg;
        private int GetLegScore(Player player) => Match.Sets.Any() && Match.Sets.Last().Legs.Any() ? Match.Sets.Last().Legs.Count(leg => leg.WinnerId == player.Id) : 0;
        
        public string Input
        {
            get
            {
                return _input;
            }
            set
            {
                _input = value;
                NotifyOfPropertyChange(() => Input);
            }
        }
        public string ViewName => nameof(ScoreboardViewModel);
        public Player Player1 => Match.Players.First();
        public Player Player2 => Match.Players[1];
        
        public int Player1LegScore { 
            get
            {
                return _player1LegScore;
            }
            set
            {
                _player1LegScore = value;
                NotifyOfPropertyChange(() => Player1LegScore);
            }
        }

        public int Player2LegScore
        {
            get
            {
                return _player2LegScore;
            }
            set
            {
                _player2LegScore = value;
                NotifyOfPropertyChange(() => Player2LegScore);
            }
        }

        public string BestOf => $"Best of {Match.Configuration.SetsToWin} sets ({Match.Configuration.LegsToWinSet} legs per set)";
        public MatchDefinition Match { get; }
        public string CurrentLeg {
            get
            {
                return _currentLeg;
            }
            set
            {
                _currentLeg = value;
                NotifyOfPropertyChange(() => CurrentLeg);
            }
        }

        public ScoreboardViewModel() {
            State.Instance.Players.Add(new Player("Thimo de Zwart"));
            State.Instance.Players.Add(new Player("Jasper van der Lugt"));
            Match = new MatchDefinition("Premier League Final 2017", DateTime.Today, new BindingList<Player>() { State.Instance.Players[0], State.Instance.Players[1] }, new BindingList<Set>(), new MatchConfiguration(5, 3, 501));
            Match.Sets.Add(new Set(new BindingList<Leg>()));
            State.Instance.Matches.Add(Match);
            SetCurrentLeg();
        }
        public BindableCollection<string> Logs { get; set; } = new BindableCollection<string>() { "Thimo de Zwart gooide T20 + T20 + T20 (180).", "Jasper van der Lugt gooide T20 + T20 + D20 (160).", "Einde leg; gewonnen door Jasper van der Lugt." };

        public void OnExit()
        {
            throw new System.NotImplementedException();
        }

        public void SetLeg()
        {
            Logs.Add("A new leg is starting!");
            _leg = new Leg(new BindingList<Turn>());
            Match.Sets.Last().Legs.Add(_leg);
        }

        public void SetScores()
        {
            Player1LegScore = GetLegScore(Player1);
            Player2LegScore = GetLegScore(Player2);
        }

        public void SetCurrentLeg()
        {
            int legCount;

            if (Match.Sets.Any() && Match.Sets.Last().Legs.Any())
            {
                legCount = Match.Sets.Last().Legs.Count;
            }
            else
            {
                legCount = 1;
            }

            CurrentLeg = $"Leg {legCount}";
        }

        public void Submit()
        {
            if (!string.IsNullOrEmpty(Input))
            {
                if (_leg == null)
                {
                    SetLeg();
                }

                if (!_leg.Turns.Any() || !_leg.Turns.Last().Valid || _leg.Turns.Last().WinningTurn || _leg.Turns.Last().Tosses.Count >= 3)
                {
                    if (_leg.Turns.Count % 2 == 0)
                    {
                        _leg.Turns.Add(new Turn(Player1, new BindingList<Toss>()));
                    }
                    else
                    {
                        _leg.Turns.Add(new Turn(Player2, new BindingList<Toss>()));
                    }

                    var player = _leg.Turns.Last().PlayerId.ToPlayer();

                    Logs.Add($"Player {player.Name} is starting their turn - {Match.Configuration.ScoreToWinLeg - _leg.Turns.Where(turn => turn.PlayerId == _leg.Turns.Last().PlayerId && turn.Valid).Sum(turn => turn.TurnScore)} left.");
                }

                var currentTurn = _leg.Turns.Last();
                var activePlayer = currentTurn.PlayerId.ToPlayer();
                int currentPlayerScore = _leg.Turns.Where(turn => turn.PlayerId == activePlayer.Id && turn.Valid).Sum(turn => turn.TurnScore);

                // Process turn
                if (currentTurn.Tosses.Count < 3 && Parser.ParseThrow(Input) != null)
                {
                    var toss = Parser.ParseThrow(Input);

                    if (currentPlayerScore + toss.TotalScore < Match.Configuration.ScoreToWinLeg)
                    {
                        currentTurn.Tosses.Add(toss);
                    }
                    else if ((currentPlayerScore + toss.TotalScore) == Match.Configuration.ScoreToWinLeg && toss.Multiplier == 2)
                    {
                        currentTurn.Tosses.Add(toss);
                        currentTurn.WinningTurn = true;
                        _leg.WinnerId = activePlayer.Id;
                    }
                    else if (
                        ((currentPlayerScore + toss.TotalScore) == Match.Configuration.ScoreToWinLeg && toss.Multiplier != 2) ||
                        (currentPlayerScore + toss.TotalScore) > Match.Configuration.ScoreToWinLeg || 
                        (currentPlayerScore % 2 == 1 && currentTurn.Tosses.Count == 2))
                    {
                        currentTurn.Tosses.Add(toss);
                        currentTurn.Valid = false;
                    }

                    Logs.Add($"Threw {toss.TotalScore}! {Match.Configuration.ScoreToWinLeg - (currentPlayerScore + toss.TotalScore)} left.");
                }


                if (!currentTurn.Valid)
                {
                    Logs.Add($"{activePlayer.Name} scores 0 points!");
                }
                else if (!currentTurn.WinningTurn && currentTurn.Tosses.Count == 3)
                {
                    Logs.Add($"{activePlayer.Name} scores {currentTurn.TurnScore} points!");
                }
                else if (currentTurn.WinningTurn)
                {
                    var winner = _leg.WinnerId.ToPlayer();
                    Logs.Add($"{winner.Name} wins the leg!");
                    SetLeg();
                    SetCurrentLeg();
                    SetScores();
                }

                //Input = string.Empty;
            }
        }
        public void Submit(KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Submit();
            }
        }
    }
}
