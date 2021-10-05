using Caliburn.Micro;
using Dartin.Abstracts;
using Dartin.Managers;
using Dartin.Models;
using System;
using System.Collections.Generic;
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

        private string _input;

        public string Input
        {
            get { return _input; }
            set { 
                _input = value;
                NotifyOfPropertyChange(() => Input);
            }
        }

        public string BestOf => $"Best of {Match.SetsToWin} sets ({Match.LegsToWinSet} legs per set)";
        public MatchDefinition Match { get; }
        public string CurrentLeg {
            get
            {
                string prefix = "Leg ";
                int currentLeg;
                if (Match.Sets.Any() && Match.Sets.Last().Legs.Any())
                    currentLeg = Match.Sets.Last().Legs.Count;
                else
                    currentLeg = 1;

                return prefix + currentLeg;
            }
        }

        public ScoreboardViewModel() {
            Match = new MatchDefinition
            {
                Name = "Premier League Final 2017",
                Date = DateTime.Today,
                SetsToWin = 5,
                LegsToWinSet = 3,
                ScoreToWinLeg = 501,
                Players = new List<Player>() { new Player { Name = "Thimo de Zwart" }, new Player { Name = "Jasper van der Lugt" } },
                Sets = new List<Set>()
            };
        }
        public BindableCollection<string> Logs { get; set; } = new BindableCollection<string>() { "Thimo de Zwart gooide T20 + T20 + T20 (180).", "Jasper van der Lugt gooide T20 + T20 + D20 (160).", "Einde leg; gewonnen door Jasper van der Lugt." };

        /// <summary>
        /// Submit score.
        /// </summary>
        public void Submit()
        {

        }

        public void OnExit()
        {
            throw new System.NotImplementedException();
        }
    }
}
