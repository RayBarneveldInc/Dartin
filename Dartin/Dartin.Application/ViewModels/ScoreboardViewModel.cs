using Caliburn.Micro;
using Dartin.Abstracts;
using Dartin.Managers;
using Dartin.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Dartin.ViewModels
{
    class ScoreboardViewModel : Screen, IViewModel
    {
        private int GetLegScore(Player player) => Match.Sets.Any() && Match.Sets.Last().Legs.Any() ? Match.Sets.Last().Legs.Count(leg => leg.Winner == player) : 0;
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
            Match = new MatchDefinition("Premier League Final 2017", DateTime.Today, new BindingList<Player>() { new Player("Thimo de Zwart"), new Player("Jasper van der Lugt") }, new BindingList<Set>(), new MatchConfiguration(5, 3, 501));
        }
        public BindableCollection<string> Logs { get; set; } = new BindableCollection<string>() { "Thimo de Zwart gooide T20 + T20 + T20 (180).", "Jasper van der Lugt gooide T20 + T20 + D20 (160).", "Einde leg; gewonnen door Jasper van der Lugt." };

        public void OnExit()
        {
            throw new System.NotImplementedException();
        }
    }
}
