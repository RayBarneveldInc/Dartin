using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Caliburn.Micro;
using Dartin;
using Dartin.Models;
using Dartin.ViewModels;
using System.ComponentModel;
using System.Linq;

namespace UnitTests
{
    public class MatchReportStatsTest
    {


        [Fact]
        public void averageScoreFirstNine()
        {
            MatchDefinition testMatch = matchDef();
            MatchStatsPlayer player1Stats = playerStats(testMatch);
            Assert.Equal(501, player1Stats.AvgScoreFirstNineDarts);
        }

        [Fact]
        public void averageScore()
        {
            MatchDefinition testMatch = matchDef();
            MatchStatsPlayer player1Stats = playerStats(testMatch);
            Assert.Equal(55, (int)player1Stats.avgScore);
        }

        [Fact]
        public void dartsThrown()
        {
            MatchDefinition testMatch = matchDef();
            MatchStatsPlayer player1Stats = playerStats(testMatch);
            Assert.Equal(9, player1Stats.dartsThrown);
        }

        [Fact]
        public void NinedartTest()
        {
            MatchDefinition testMatch = matchDef();
            MatchStatsPlayer player1Stats = playerStats(testMatch);
            Assert.Equal(1, player1Stats.nineDarters);
        }

        [Fact]
        public void HundredEighties()
        {
            MatchDefinition testMatch = matchDef();
            MatchStatsPlayer player1Stats = playerStats(testMatch);
            Assert.Equal(2, player1Stats.hundredEighty);
        }

        [Fact]
        public void HundredSixtyPlus()
        {
            MatchDefinition testMatch = matchDef();
            MatchStatsPlayer player1Stats = playerStats(testMatch);
            Assert.Equal(0, player1Stats.hundredSixtyPlus);
        }

        [Fact]
        public void hundredFourtyPlus()
        {
            MatchDefinition testMatch = matchDef();
            MatchStatsPlayer player1Stats = playerStats(testMatch);
            Assert.Equal(1, player1Stats.hundredFourtyPlus);
        }

        [Fact]
        public void hundredTwentyPlus()
        {
            MatchDefinition testMatch = matchDef();
            MatchStatsPlayer player1Stats = playerStats(testMatch);
            Assert.Equal(0, player1Stats.hundredTwentyPlus);
        }

        [Fact]
        public void hundredPlus()
        {
            MatchDefinition testMatch = matchDef();
            MatchStatsPlayer player1Stats = playerStats(testMatch);
            Assert.Equal(0, player1Stats.hundredPlus);
        }

        [Fact]
        public void setsWon()
        {
            MatchDefinition testMatch = matchDef();
            MatchStatsPlayer player1Stats = playerStats(testMatch);
            Assert.Equal(1, player1Stats.setsWon);
        }

        [Fact]
        public void legsWon()
        {
            MatchDefinition testMatch = matchDef();
            MatchStatsPlayer player1Stats = playerStats(testMatch);
            Assert.Equal(1, player1Stats.legsWon);
        }


        public MatchDefinition matchDef()
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

            BindingList<Toss> gooien2 = new BindingList<Toss>();
            gooien2.Add(t4);
            gooien2.Add(t5);
            gooien2.Add(t6);
            Turn tu2 = new Turn(p1, gooien2);

            BindingList<Toss> gooien3 = new BindingList<Toss>();
            gooien3.Add(t7);
            gooien3.Add(t8);
            gooien3.Add(t9);
            Turn tu3 = new Turn(p1, gooien3);

            BindingList<Turn> turnss = new BindingList<Turn>();
            turnss.Add(tu);
            turnss.Add(tu2);
            turnss.Add(tu3);

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

            //MatchConfiguration configlol = new MatchConfiguration(2, 2, 501);
            //return new MatchDefinition("Lmfao", DateTime.Now, spelers, sets, configlol);
        }

        public MatchStatsPlayer playerStats(MatchDefinition match)
        {
            return new MatchStatsPlayer(match);
        }
        
    }
}
