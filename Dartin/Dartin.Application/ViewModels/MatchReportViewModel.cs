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

namespace Dartin.ViewModels
{
    class MatchReportViewModel : Screen, IViewModel
    {
        public string ViewName { get; }
        private MatchDefinition _currentMatch;

        public BindingList<MatchDefinition> ExistingMatches => State.Instance.Matches;
        public int MatchCount => State.Instance.Matches.Count;
        
        public void ListViewSelection(object sender, MouseButtonEventArgs e)
        {
            var item = sender as MatchDefinition;
            if (item != null)
            {
                changeMatch(item);
            }
        }

        public void changeMatch(MatchDefinition selectedMatch)
        {
            _currentMatch = selectedMatch; 
        }

        public void OnExit()
        {
            throw new NotImplementedException();
        }


        public MatchReportViewModel()
        {
            _currentMatch = State.Instance.Matches[2];
            //_currentMatch = new MatchDefinition();
            //Player p1 = new Player();
            //Player p2 = new Player();
            //p1.Name = "Jacco";
            //p2.Name = "Tjeerd";
            //_currentMatch.Players.Add(p1);
            //_currentMatch.Players.Add(p2);
            //_currentMatch.Players[1] = p2;

            //Toss t1 = new Toss();
            //t1.Score = 20;
            //t1.Multiplier = 3;

            //Toss t2 = new Toss();
            //t2.Score = 20;
            //t2.Multiplier = 2;

            //Toss t3 = new Toss();
            //t3.Score = 20;
            //t3.Multiplier = 1;

            //Turn tu = new Turn();
            //tu.Player = p1;
            //tu.TurnScore = 120;
            //tu.Tosses.Add(t1);
            //tu.Tosses.Add(t2);
            //tu.Tosses.Add(t3);

            //Leg l = new Leg();
            //l.Turns.Add(tu);
            ////l.Winner.Name = p2.Name;

            //Set s = new Set();
            //s.Legs = new List<Leg>();
            //s.LegsToWinSet = 1;

            //s.Legs.Add(l);
            //s.Winner = p2;

            //_currentMatch.Name = "Lmfao";
            //_currentMatch.Sets.Add(s);


            //State.Instance.Matches.Add(_currentMatch);
        }

        public MatchDefinition Match => _currentMatch;

        public string PlayerOne => PlayerString(0);
        public string PlayerTwo => PlayerString(1);

        public string PlayerString(int id)
        {
            if (_currentMatch != null)
            {
                return _currentMatch.Players[id].Name;

            } else
            {
                return "Willem" + id;
            }
        }
        


    }
}
