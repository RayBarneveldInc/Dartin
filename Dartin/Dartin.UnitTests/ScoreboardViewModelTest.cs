using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dartin.ViewModels;
using Dartin.Models;
using Xunit;
using System.ComponentModel;
using Dartin;
using System.Diagnostics;
using System.Globalization;

namespace UnitTests
{
    public class ScoreboardViewModelTest
    {
        private void ClearState()
        {
            State.Instance.Matches.Clear();
            State.Instance.Players.Clear();
        }

        private void SubmitTossInputs(ScoreboardViewModel vm, string inputOne, string inputTwo, string inputThree)
        {
            vm.TossOneInput = inputOne;
            vm.TossTwoInput = inputTwo;
            vm.TossThreeInput = inputThree;

            vm.Submit();
        }

        [Fact]
        public void TestSetLeg()
        {
            ClearState();

            var vm = new ScoreboardViewModel(CreateMatchDefinitionWithPlayers());
            vm.SetLeg();

            Assert.Single(vm.Match.Sets.Last().Legs);
        }

        [Fact]
        public void TestSetSet()
        {
            ClearState();

            var vm = new ScoreboardViewModel(CreateMatchDefinitionWithPlayers());
            vm.SetSet();

            Assert.Equal(2, vm.Match.Sets.Count);
        }

        [Fact]
        public void TestGetLegScore()
        {
            ClearState();

            var vm = new ScoreboardViewModel(CreateMatchDefinitionWithPlayers());
            Player player = vm.Match.Players.First();
            int resultOne = vm.GetLegScore(player);

            Leg legOne = new Leg(new BindingList<Turn>());
            Leg legTwo = new Leg(new BindingList<Turn>());
            Leg legThree = new Leg(new BindingList<Turn>());

            legOne.WinnerId = player.Id;
            legTwo.WinnerId = player.Id;
            legThree.WinnerId = player.Id;

            vm.Match.Sets.Last().Legs.Add(legOne);
            vm.Match.Sets.Last().Legs.Add(legTwo);
            vm.Match.Sets.Last().Legs.Add(legThree);

            int resultTwo = vm.GetLegScore(player);

            Assert.Equal(0, resultOne);
            Assert.Equal(3, resultTwo);
        }
        
        [Fact]
        public void TestGetSetScore()
        {
            ClearState();

            var vm = new ScoreboardViewModel(CreateMatchDefinitionWithPlayers());
            Player player = vm.Match.Players.First();
            int resultOne = vm.GetSetScore(player);

            Set setOne = new Set(new BindingList<Leg>());
            Set setTwo = new Set(new BindingList<Leg>());
            Set setThree = new Set(new BindingList<Leg>());

            setOne.WinnerId = player.Id;
            setTwo.WinnerId = player.Id;
            setThree.WinnerId = player.Id;

            vm.Match.Sets.Add(setOne);
            vm.Match.Sets.Add(setTwo);
            vm.Match.Sets.Add(setThree);

            int resultTwo = vm.GetSetScore(player);

            Assert.Equal(0, resultOne);
            Assert.Equal(3, resultTwo);
        }


        // BUG: Is broken out of range exception 
        
        //[Fact]
        //public void TestStartPlayerTurn()
        //{
        //    ClearState();

        //    // This test should also test for turn after set or leg is won.

        //    var vm = new ScoreboardViewModel(new MatchDefinition());
        //    vm.SetLeg();

        //    Player playerOne = vm.Player1;
        //    Player playerTwo = vm.Player2;

        //    Turn turn = vm.StartPlayerTurn();
        //    var turns = vm.Match.Sets.Last().Legs.Last().Turns;

        //    Assert.Single(turns);
        //    Assert.True(turn.PlayerId == playerOne.Id);

        //    turn.Tosses.Add(new Toss(10, 2));
        //    turn.Tosses.Add(new Toss(10, 2));
        //    turn.Tosses.Add(new Toss(10, 2));
        //    turn = vm.StartPlayerTurn();
        //    turns = vm.Match.Sets.Last().Legs.Last().Turns;

        //    Assert.Equal(2, turns.Count);
        //    Assert.True(turn.PlayerId == playerTwo.Id);
        //}

        [Fact]
        public void TestComparePlayerScoreWithScoreToWinLeg()
        {
            ClearState();

            var vm = new ScoreboardViewModel(CreateMatchDefinitionWithPlayers());
            vm.Match.ScoreToWinLeg = 501;
            bool result = vm.ComparePlayerScoreWithScoreToWinLeg(180, new Toss(20, 3));

            Assert.False(result);

            result = vm.ComparePlayerScoreWithScoreToWinLeg(465, new Toss(18, 2));

            Assert.True(result);
        }


        //TODO FIX TEST
        //[Fact]
        //public void TestGetPlayerRemainders()
        //{
        //    ClearState();

        //    var vm = new ScoreboardViewModel(CreateMatchDefinitionWithPlayers());

        //    vm.Match.ScoreToWinLeg = 501;

        //    SubmitTossInputs(vm, "t20", "t20", "t20");
        //    SubmitTossInputs(vm, "t20", "t10", "t5");
        //    SubmitTossInputs(vm, "t5", "d18", "d5");
        //    SubmitTossInputs(vm, "d19", "t15", "10");

        //    Leg leg = vm.Match.Sets.Last().Legs.Last();

        //    List<int> remainders = leg.GetRemaindersForPlayer(vm.Player1, vm.Match.ScoreToWinLeg);

        //    Assert.Equal(321, remainders[0]);
        //    Assert.Equal(260, remainders[1]);

        //    remainders = leg.GetRemaindersForPlayer(vm.Player2, vm.Match.ScoreToWinLeg);

        //    Assert.Equal(396, remainders[0]);
        //    Assert.Equal(303, remainders[1]);
        //}

        private static MatchDefinition CreateMatchDefinitionWithPlayers()
        {
            MatchDefinition matchDefinition = new MatchDefinition();
            for (int i = 0; i < 2; i++)
            {
                Player p = new Player()
                {
                    FirstName = "Test",
                    LastName = "Player " + i.ToString(CultureInfo.CurrentCulture)
                };
                matchDefinition.Players.Add(p);
            }
            return matchDefinition;
        }

        // TODO FIX TEST
        //[Fact] 
        //public void TestSubmit()
        //{
        //    ClearState();

        //    var vm = new ScoreboardViewModel(CreateMatchDefinitionWithPlayers());

        //    SubmitTossInputs(vm, "t20", "t20", "t20");
        //    SubmitTossInputs(vm, "t20", "t20", "t20");
        //    SubmitTossInputs(vm, "t20", "t20", "t20");
        //    SubmitTossInputs(vm, "t20", "t20", "t20");
        //    SubmitTossInputs(vm, "t20", "t15", "d18");

        //    Assert.Single(vm.Match.Sets.Last().Legs.Where(leg => leg.WinnerId == vm.Player1.Id));
        //}
    }
}
