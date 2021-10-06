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

namespace Dartin.ViewModels
{
    class ScoreboardViewModel : Screen, IViewModel
    {
        private Leg _leg;
        private int GetLegScore(Player player) => Match.Sets.Any() && Match.Sets.Last().Legs.Any() ? Match.Sets.Last().Legs.Count(leg => leg.WinnerId == player.Id) : 0;
        public string Input { get; set; }
        public string ViewName => nameof(ScoreboardViewModel);
        public Player Player1 => Match.Players.First();
        public Player Player2 => Match.Players[1];
        public int Player1LegScore => GetLegScore(Player1);
        public int Player2LegScore => GetLegScore(Player2);

        public string BestOf => $"Best of {Match.Configuration.SetsToWin} sets ({Match.Configuration.LegsToWinSet} legs per set)";
        public MatchDefinition Match { get; }
        public string CurrentLeg {
            get
            {
                string prefix = "Leg ";
                int currentLeg;
                if (Match.Sets.Any() && Match.Sets.Last().Legs.Any())
                {
                    currentLeg = Match.Sets.Last().Legs.Count;
                }
                else
                {
                    currentLeg = 1;
                }

                return prefix + currentLeg;
            }
        }

        public ScoreboardViewModel() {
            State.Instance.Players.Add(new Player("Thimo de Zwart"));
            State.Instance.Players.Add(new Player("Jasper van der Lugt"));
            Match = new MatchDefinition("Premier League Final 2017", DateTime.Today, new BindingList<Player>() { State.Instance.Players[0], State.Instance.Players[1] }, new BindingList<Set>(), new MatchConfiguration(5, 3, 501));
            State.Instance.Matches.Add(Match);
        }
        public BindableCollection<string> Logs { get; set; } = new BindableCollection<string>() { "Thimo de Zwart gooide T20 + T20 + T20 (180).", "Jasper van der Lugt gooide T20 + T20 + D20 (160).", "Einde leg; gewonnen door Jasper van der Lugt." };

        public void OnExit()
        {
            throw new System.NotImplementedException();
        }

        public void Submit()
        {
            if (_leg == null || (_leg.Turns.Any() && _leg.Turns.Last().WinningTurn))
            {
                Logs.Add("A new leg is starting!");
                _leg = new Leg(new BindingList<Turn>());
            }

            if (!_leg.Turns.Any() || !_leg.Turns.Last().Valid || _leg.Turns.Last().WinningTurn || _leg.Turns.Last().Tosses.Count >= 3)
            {
                if (_leg.Turns.Count % 2 == 0)
                    _leg.Turns.Add(new Turn(Player1, new BindingList<Toss>()));
                else
                    _leg.Turns.Add(new Turn(Player2, new BindingList<Toss>()));

                if (_leg.Turns.Last().PlayerId.TryResolveToPlayer(out Player p))
                    Logs.Add($"Player {p.Name} is starting their turn - {Match.Configuration.ScoreToWinLeg - _leg.Turns.Where(turn => turn.PlayerId == _leg.Turns.Last().PlayerId).Sum(turn => turn.TurnScore)} left.");
            }

            var currentTurn = _leg.Turns.Last();

            int currentPlayerScore = _leg.Turns.Where(turn => turn.PlayerId == currentTurn.PlayerId && turn.Valid).Sum(turn => turn.TurnScore);

            // Process turn
            if (currentTurn.Tosses.Count < 3)
            {
                if (Parser.TryParseThrow(Input, out Toss toss))
                {
                    if (currentPlayerScore + toss.TotalScore < Match.Configuration.ScoreToWinLeg)
                    {
                        currentTurn.Tosses.Add(toss);
                    }
                    else if ((currentPlayerScore + toss.TotalScore) == Match.Configuration.ScoreToWinLeg && toss.Multiplier == 2)
                    {
                        currentTurn.Tosses.Add(toss);
                        currentTurn.WinningTurn = true;
                        _leg.WinnerId = currentTurn.PlayerId;
                    }
                    else if (currentPlayerScore % 2 == 1 && currentTurn.Tosses.Count == 2)
                    {
                        currentTurn.Tosses.Add(toss);
                        currentTurn.Valid = false;
                    }

                    Logs.Add($"Threw {toss.TotalScore}! {Match.Configuration.ScoreToWinLeg - (currentPlayerScore + toss.TotalScore)} left.");
                }
            }

            if (!currentTurn.WinningTurn && currentTurn.Tosses.Count == 3 && currentTurn.PlayerId.TryResolveToPlayer(out Player player))
                Logs.Add($"{player.Name} scores {currentTurn.TurnScore} points!");
            else if (currentTurn.WinningTurn && currentTurn.PlayerId.TryResolveToPlayer(out Player winner))
                Logs.Add($"{winner.Name} wins the leg!");
        }
        public void Submit(KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Space)
                Submit();
        }
    }
}
