using Caliburn.Micro;
using Dartin.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using Newtonsoft.Json;
using System.Windows;
using Dartin.Extensions;
using Dartin.Managers;

namespace Dartin.ViewModels
{
    class MatchReportViewModel : Screen, IViewModel
    {
        private MatchDefinition _currentMatch;
        public MatchDefinition Match => _currentMatch;
        public Player PlayerOne => Match.Players[0].ToPlayer();
        public Player PlayerTwo => Match.Players[1].ToPlayer();
        public MatchDefinition MatchInfo1 => SetsPerPlayer(0);
        public MatchDefinition MatchInfo2 => SetsPerPlayer(1);
        public MatchStatsPlayer player1Stats { get; set; }
        public MatchStatsPlayer player2Stats { get; set; }

        public void OnExit()
        {
            throw new NotImplementedException();
        }

        public MatchDefinition SetsPerPlayer(int i)
        {
            var copyser = JsonConvert.SerializeObject(Match, Formatting.Indented);
            MatchDefinition deepcopy = JsonConvert.DeserializeObject<MatchDefinition>(copyser);
            Guid playerId = deepcopy.Players[i];
            deepcopy.Players.Clear();
            deepcopy.Players.Add(playerId);

            foreach (Set set in deepcopy.Sets)
            {
                foreach (Leg leg in set.Legs)
                {
                    leg.Turns = new BindingList<Turn>(leg.Turns.Where(t => t.PlayerId.Equals(playerId)).ToList());
                }
            }
            return deepcopy;
        }

        public void Back()
        {
            ScreenManager.GetInstance().SwitchViewModel(new MatchesViewModel(State.Instance.Matches));
        }

        public MatchReportViewModel(MatchDefinition match)
        {
            _currentMatch = match;
            App.Current.Properties["playeroneID"] = PlayerOne.Id;
            App.Current.Properties["playertwoID"] = PlayerTwo.Id;
            player1Stats = new MatchStatsPlayer(MatchInfo1);
            player2Stats = new MatchStatsPlayer(MatchInfo2);
        }
    }
}
