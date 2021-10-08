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
        private string _tossOneInput;
        private string _tossTwoInput;
        private string _tossThreeInput;
        private string _currentLeg;
        private BindableCollection<Turn> _player1Turns;
        private BindableCollection<Turn> _player2Turns;
        private int _player1Remainder;
        private int _player2Remainder;

        private int GetLegScore(Player player) => Match.Sets.Any() && Match.Sets.Last().Legs.Any() ? Match.Sets.Last().Legs.Count(leg => leg.WinnerId == player.Id) : 0;
        
        public string TossOneInput
        {
            get
            {
                return _tossOneInput;
            }
            set
            {
                _tossOneInput = value;
                NotifyOfPropertyChange(() => TossOneInput);
            }
        }

        public string TossTwoInput
        {
            get
            {
                return _tossTwoInput;
            }
            set
            {
                _tossTwoInput = value;
                NotifyOfPropertyChange(() => TossTwoInput);
            }
        }

        public string TossThreeInput
        {
            get
            {
                return _tossThreeInput;
            }
            set
            {
                _tossThreeInput = value;
                NotifyOfPropertyChange(() => TossThreeInput);
            }
        }

        public string ViewName => nameof(ScoreboardViewModel);
        public Player Player1 => Match.Players.First();
        public Player Player2 => Match.Players[1];
        public BindableCollection<Turn> Player1Turns
        {
            get => _player1Turns;
            set
            {
                _player1Turns = value;
                NotifyOfPropertyChange(() => Player1Turns);
            }
        }
        public BindableCollection<Turn> Player2Turns
        {
            get => _player2Turns;
            set
            {
                _player2Turns = value;
                NotifyOfPropertyChange(() => Player2Turns);
            }
        }
        
        public int Player1LegScore
        { 
            get => _player1LegScore;
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

        public int Player1Remainder {
            get => _player1Remainder;
            set
            {
                _player1Remainder = value;
                NotifyOfPropertyChange(() => Player1Remainder);
            }
        }

        public int Player2Remainder {
            get => _player2Remainder;
            set
            {
                _player2Remainder = value;
                NotifyOfPropertyChange(() => Player1Remainder);
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
            Player1Remainder = Match.Configuration.ScoreToWinLeg;
            Player2Remainder = Match.Configuration.ScoreToWinLeg;
            SetCurrentLeg();
        }

        public void OnExit()
        {
            throw new System.NotImplementedException();
        }

        public void SetLeg()
        {
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

        public Player StartPlayerTurn()
        {
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
            }

            return _leg.Turns.Last().PlayerId.ToPlayer();
        }

        public void PrcocessTossInputTurn(string tossInput)
        {
            if (!string.IsNullOrEmpty(tossInput) && Parser.ParseThrow(tossInput) != null)
            {
                Toss toss = Parser.ParseThrow(tossInput);
                Turn currentTurn = GetCurrentTurn();
                Player activePlayer = GetActivePlayer();
                int currentPlayerScore = GetCurrentPlayerScore();

                if (currentPlayerScore + toss.TotalScore < Match.Configuration.ScoreToWinLeg)
                {
                    currentTurn.Tosses.Add(toss);
                }
                else if (ComparePlayerScoreWithScoreToWinLeg(currentPlayerScore, toss) && toss.Multiplier == 2)
                {
                    currentTurn.Tosses.Add(toss);
                    currentTurn.WinningTurn = true;
                    _leg.WinnerId = activePlayer.Id;
                }
                else if (
                    (ComparePlayerScoreWithScoreToWinLeg(currentPlayerScore, toss) && toss.Multiplier != 2) ||
                    (currentPlayerScore + toss.TotalScore) > Match.Configuration.ScoreToWinLeg)
                {
                    currentTurn.Tosses.Add(toss);
                    currentTurn.Valid = false;
                }
            }
        }

        public bool ComparePlayerScoreWithScoreToWinLeg(int currentPlayerScore, Toss toss)
        {
            return (currentPlayerScore + toss.TotalScore) == Match.Configuration.ScoreToWinLeg;
        }

        public Turn GetCurrentTurn()
        {
            return _leg.Turns.Last();
        }

        public Player GetActivePlayer()
        {
            return GetCurrentTurn().PlayerId.ToPlayer();
        }

        public int GetCurrentPlayerScore()
        {
            Player activePlayer = GetActivePlayer();

            return _leg.Turns.Where(turn => turn.PlayerId == activePlayer.Id && turn.Valid).Sum(turn => turn.Score);
        }

        public int CalculatePlayerScoreLeft()
        {
            return Match.Configuration.ScoreToWinLeg - _leg.Turns.Where(turn => turn.PlayerId == _leg.Turns.Last().PlayerId && turn.Valid).Sum(turn => turn.Score);
        }

        public void HandlePlayerScore()
        {
            Player activePlayer = GetActivePlayer();

            if (activePlayer.Id == Player1.Id)
            {
                Player1Turns = new BindableCollection<Turn>(Match.Sets.Last().Legs.SelectMany(leg => leg.Turns).Where(turn => turn.PlayerId == Player1.Id && turn.Valid));
                Player1Remainder = Match.Configuration.ScoreToWinLeg - Player1Turns.Sum(turn => turn.Score);
            }
            else
            {
                Player2Turns = new BindableCollection<Turn>(Match.Sets.Last().Legs.SelectMany(leg => leg.Turns).Where(turn => turn.PlayerId == Player2.Id && turn.Valid));
                Player2Remainder = Match.Configuration.ScoreToWinLeg - Player2Turns.Sum(turn => turn.Score);
            }
        }

        public void HandleEndTurn()
        {
            Turn currentTurn = GetCurrentTurn();
            Player activePlayer = GetActivePlayer();

            if (!currentTurn.Valid)
            {
                Debug.WriteLine($"{activePlayer.Name} scores 0 points!");
            }
            else if (!currentTurn.WinningTurn && currentTurn.Tosses.Count == 3)
            {
                Debug.WriteLine($"{activePlayer.Name} scores {currentTurn.Score} points!");
            }
            else if (currentTurn.WinningTurn)
            {
                Player winner = _leg.WinnerId.ToPlayer();

                Debug.WriteLine($"{winner.Name} wins the leg!");
                SetLeg();
                SetCurrentLeg();
                SetScores();
            }
        }

        public void Submit()
        {
            if (_leg == null)
            {
                SetLeg();
            }

            StartPlayerTurn();

            Turn currentTurn = GetCurrentTurn();

            PrcocessTossInputTurn(TossOneInput);
            PrcocessTossInputTurn(TossTwoInput);
            PrcocessTossInputTurn(TossThreeInput);

            if (currentTurn.Tosses.Any())
            {
                HandlePlayerScore();
                HandleEndTurn();
            }

            //Logs.Add($"Threw {toss.TotalScore}! {Match.Configuration.ScoreToWinLeg - (currentPlayerScore + toss.TotalScore)} left.");
        }

        public void ClearTossInputs()
        {
            TossOneInput = string.Empty;
            TossTwoInput = string.Empty;
            TossThreeInput = string.Empty;
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
