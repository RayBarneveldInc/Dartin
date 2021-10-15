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

namespace Dartin.ViewModels
{
    class MatchReportViewModel : Screen, IViewModel
    {
        private MatchDefinition _currentMatch;

        public string ViewName { get; }
        public MatchDefinition Match => _currentMatch;
        public string PlayerOne => PlayerString(0);
        public string PlayerTwo => PlayerString(1);
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

            Player player = deepcopy.Players[i];

            foreach (Set s in deepcopy.Sets)
            {
                s.Index = deepcopy.Sets.IndexOf(s);
                foreach (Leg l in s.Legs)
                {
                    l.Turns = new BindingList<Turn>(l.Turns.Where(t => t.PlayerId.Equals(player.Id)).Cast<Turn>().ToList());
                    l.Index = s.Legs.IndexOf(l);
                    foreach (Turn t in l.Turns)
                    {
                        t.Index = l.Turns.IndexOf(t);
                        foreach (Toss to in t.Tosses)
                        {
                            to.Index = t.Tosses.IndexOf(to);
                        }
                    }
                }
            }
            return deepcopy;
        }

        public MatchReportViewModel()
        {
            _currentMatch = State.Instance.Matches[0];
            player1Stats = new MatchStatsPlayer(MatchInfo1);
            player2Stats = new MatchStatsPlayer(MatchInfo2);

        }



        public string PlayerString(int id)
        {
            return _currentMatch.Players[id].Name;
        }



    }
}
