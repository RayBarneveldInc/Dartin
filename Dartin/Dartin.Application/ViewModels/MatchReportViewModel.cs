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
        public Player playerOne => Match.Players[0];
        public string PlayerTwo => PlayerString(1);
        public Player playerTwo => Match.Players[1];
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
            deepcopy.Players.Clear();
            deepcopy.Players.Add(player);

            foreach (Set s in deepcopy.Sets)
            {
                foreach (Leg l in s.Legs)
                {
                    l.Turns = new BindingList<Turn>(l.Turns.Where(t => t.PlayerId.Equals(player.Id)).Cast<Turn>().ToList());
                }
            }
            return deepcopy;
        }

        public MatchReportViewModel(
            //MatchDefinition match
            )
        {
            Player p1 = new Player("Jacco", "Blokje");
            Player p2 = new Player("Tjeerd", "Geld");

            BindingList<Player> spelers = new BindingList<Player>();
            spelers.Add(p1);
            spelers.Add(p2);

            Toss t1 = new Toss(20, 3);
            Toss t2 = new Toss(20, 3);
            Toss t3 = new Toss(20, 3);

            Toss t4 = new Toss(20, 3);
            Toss t5 = new Toss(20, 3);
            Toss t6 = new Toss(20, 3);

            Toss t7 = new Toss(20, 3);
            Toss t8 = new Toss(19, 3);
            Toss t9 = new Toss(12, 2);

            BindingList<Toss> gooien1 = new BindingList<Toss>();
            gooien1.Add(t1);
            gooien1.Add(t2);
            gooien1.Add(t3);
            Turn tu = new Turn(p1, gooien1);
            Turn tup2 = new Turn(p2, gooien1);

            BindingList<Toss> gooien2 = new BindingList<Toss>();
            gooien2.Add(t4);
            gooien2.Add(t5);
            gooien2.Add(t6);
            Turn tu2 = new Turn(p1, gooien2);
            Turn tu2p2 = new Turn(p2, gooien2);

            BindingList<Toss> gooien3 = new BindingList<Toss>();
            gooien3.Add(t7);
            gooien3.Add(t8);
            gooien3.Add(t9);
            Turn tu3 = new Turn(p1, gooien3);
            Turn tu3p2 = new Turn(p2, gooien3);

            BindingList<Turn> turnss = new BindingList<Turn>();
            BindingList<Turn> turnssp2 = new BindingList<Turn>();
            turnss.Add(tu);
            turnss.Add(tu2);
            turnss.Add(tu3);
            turnss.Add(tup2);
            turnss.Add(tu2p2);
            turnss.Add(tu3p2);

            BindingList<Leg> legs = new BindingList<Leg>();
            Leg legje = new Leg(turnss);
            legje.WinnerId = p1.Id;
            legje.Winner = p1;
            legs.Add(legje);

            BindingList<Set> sets = new BindingList<Set>();
            Set setje = new Set(legs);
            setje.WinnerId = p1.Id;
            setje.Winner = p1;
            sets.Add(setje);
            sets.Add(setje);

            MatchDefinition testMatch = new MatchDefinition();
            testMatch.Players = spelers;
            testMatch.Sets = sets;

            _currentMatch = testMatch;
            App.Current.Properties["playeroneID"] = p1.Id;
            App.Current.Properties["playertwoID"] = p2.Id;
            player1Stats = new MatchStatsPlayer(MatchInfo1);
            player2Stats = new MatchStatsPlayer(MatchInfo2);

        }

        public string PlayerString(int id)
        {
            return _currentMatch.Players[id].Name;
        }



    }
}
