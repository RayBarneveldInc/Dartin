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
        public void TestAverageScoreFirstNine()
        {
            MatchDefinition testMatch = MatchDef();
            MatchStatsPlayer player1Stats = PlayerStats(testMatch);
            Assert.Equal(501, player1Stats.AvgScoreFirstNineDarts);
        }

        [Fact]
        public void TestAverageScore()
        {
            MatchDefinition testMatch = MatchDef();
            MatchStatsPlayer player1Stats = PlayerStats(testMatch);
            Assert.Equal(55, (int)player1Stats.AvgScore);
        }

        [Fact]
        public void TestDartsThrown()
        {
            MatchDefinition testMatch = MatchDef();
            MatchStatsPlayer player1Stats = PlayerStats(testMatch);
            Assert.Equal(9, player1Stats.DartsThrown);
        }

        [Fact]
        public void TestNineDarters()
        {
            MatchDefinition testMatch = MatchDef();
            MatchStatsPlayer player1Stats = PlayerStats(testMatch);
            Assert.Equal(1, player1Stats.NineDarters);
        }

        [Fact]
        public void TestHundredEighties()
        {
            MatchDefinition testMatch = MatchDef();
            MatchStatsPlayer player1Stats = PlayerStats(testMatch);
            Assert.Equal(2, player1Stats.HundredEighty);
        }

        [Fact]
        public void TestHundredSixtyPlus()
        {
            MatchDefinition testMatch = MatchDef();
            MatchStatsPlayer player1Stats = PlayerStats(testMatch);
            Assert.Equal(0, player1Stats.HundredSixtyPlus);
        }

        [Fact]
        public void TestHundredFourtyPlus()
        {
            MatchDefinition testMatch = MatchDef();
            MatchStatsPlayer player1Stats = PlayerStats(testMatch);
            Assert.Equal(1, player1Stats.HundredFourtyPlus);
        }

        [Fact]
        public void TestHundredTwentyPlus()
        {
            MatchDefinition testMatch = MatchDef();
            MatchStatsPlayer player1Stats = PlayerStats(testMatch);
            Assert.Equal(0, player1Stats.HundredTwentyPlus);
        }

        [Fact]
        public void TestHundredPlus()
        {
            MatchDefinition testMatch = MatchDef();
            MatchStatsPlayer player1Stats = PlayerStats(testMatch);
            Assert.Equal(0, player1Stats.HundredPlus);
        }

        [Fact]
        public void TestSetsWon()
        {
            MatchDefinition testMatch = MatchDef();
            MatchStatsPlayer player1Stats = PlayerStats(testMatch);
            Assert.Equal(1, player1Stats.SetsWon);
        }

        [Fact]
        public void TestLegsWon()
        {
            MatchDefinition testMatch = MatchDef();
            MatchStatsPlayer player1Stats = PlayerStats(testMatch);
            Assert.Equal(1, player1Stats.LegsWon);
        }


        public MatchDefinition MatchDef()
        {
            Player p1 = new Player("Jacco", "Blokje");
            Player p2 = new Player("Tjeerd", "Geld");

            MatchDefinition match = new MatchDefinition();

            BindingList<Player> players = new BindingList<Player>();
            players.Add(p1);
            players.Add(p2);

            Toss throw1 = new Toss(20, 3);
            Toss throw2 = new Toss(20, 3);
            Toss throw3 = new Toss(20, 3);

            Toss throw4 = new Toss(20, 3);
            Toss throw5 = new Toss(20, 3);
            Toss throw6 = new Toss(20, 3);

            Toss throw7 = new Toss(20, 3);
            Toss throw8 = new Toss(19, 3);
            Toss throw9 = new Toss(12, 2);

            BindingList<Toss> throws1 = new BindingList<Toss>();
            throws1.Add(throw1);
            throws1.Add(throw2);
            throws1.Add(throw3);
            Turn turn = new Turn(p1, throws1);

            BindingList<Toss> throws2 = new BindingList<Toss>();
            throws2.Add(throw4);
            throws2.Add(throw5);
            throws2.Add(throw6);
            Turn turn2 = new Turn(p1, throws2);

            BindingList<Toss> throws3 = new BindingList<Toss>();
            throws3.Add(throw7);
            throws3.Add(throw8);
            throws3.Add(throw9);
            Turn turn3 = new Turn(p1, throws3);

            BindingList<Turn> turns = new BindingList<Turn>();
            turns.Add(turn);
            turns.Add(turn2);
            turns.Add(turn3);

            BindingList<Leg> legs = new BindingList<Leg>();
            Leg leg = new Leg(turns);
            leg.WinnerId = p1.Id;
            leg.Winner = p1;
            legs.Add(leg);

            BindingList<Set> sets = new BindingList<Set>();
            Set set = new Set(legs);
            set.WinnerId = p1.Id;
            set.Winner = p1;
            sets.Add(set);

            match.Sets = sets;
            match.Players = players;
            match.Date = new DateTime();

            return match;
        }

        public MatchStatsPlayer PlayerStats(MatchDefinition match)
        {
            return new MatchStatsPlayer(match);
        }
        
    }
}
