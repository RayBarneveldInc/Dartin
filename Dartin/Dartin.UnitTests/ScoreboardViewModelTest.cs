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

        private void SubmitLegWinner(ScoreboardViewModel vm)
        {
            SubmitTossInputs(vm, "t20", "t20", "t20");
            SubmitTossInputs(vm, "t20", "t20", "t20");
            SubmitTossInputs(vm, "t20", "t20", "t20");
            SubmitTossInputs(vm, "t20", "t20", "t20");
            SubmitTossInputs(vm, "t20", "t15", "d18");
        }

        [Fact]
        public void TestSetLeg()
        {
            ClearState();
            var vm = new ScoreboardViewModel(TestUtility.CreateExampleMatchDefinition());

            Assert.Single(vm.Match.CurrentSet.Legs);
        }

        [Fact]
        public void TestSetSet()
        {
            ClearState();

            var vm = new ScoreboardViewModel(TestUtility.CreateExampleMatchDefinition());
            vm.AddSet();

            Assert.Equal(2, vm.Match.Sets.Count);
        }

        [Fact]
        public void TestGetLegScore()
        {
            ClearState();

            var vm = new ScoreboardViewModel(TestUtility.CreateExampleMatchDefinition());
            Guid playerId = vm.Match.Players.First();
            int resultOne = vm.Match.GetAmountOfLegsWonOnCurrentSet(playerId);

            Leg legOne = new Leg(new BindingList<Turn>());
            Leg legTwo = new Leg(new BindingList<Turn>());
            Leg legThree = new Leg(new BindingList<Turn>());

            legOne.WinnerId = playerId;
            legTwo.WinnerId = playerId;
            legThree.WinnerId = playerId;

            vm.Match.CurrentSet.Legs.Add(legOne);
            vm.Match.CurrentSet.Legs.Add(legTwo);
            vm.Match.CurrentSet.Legs.Add(legThree);

            int resultTwo = vm.Match.GetAmountOfLegsWonOnCurrentSet(playerId);

            Assert.Equal(0, resultOne);
            Assert.Equal(3, resultTwo);
        }
        
        [Fact]

        public void TestGetSetScore()
        {
            ClearState();

            var vm = new ScoreboardViewModel(TestUtility.CreateExampleMatchDefinition());
            Guid playerId = vm.Match.Players.First();
            int resultOne = vm.Match.GetAmountOfSetsWon(playerId);

            Set setOne = new Set(new BindingList<Leg>());
            Set setTwo = new Set(new BindingList<Leg>());
            Set setThree = new Set(new BindingList<Leg>());

            setOne.WinnerId = playerId;
            setTwo.WinnerId = playerId;
            setThree.WinnerId = playerId;

            vm.Match.Sets.Add(setOne);
            vm.Match.Sets.Add(setTwo);
            vm.Match.Sets.Add(setThree);

            int resultTwo = vm.Match.GetAmountOfSetsWon(playerId);

            Assert.Equal(0, resultOne);
            Assert.Equal(3, resultTwo);
        }

        [Fact]
        public void TestStartPlayerTurn()
        {

            ClearState();

            // This test should also test for turn after set or leg is won.

            var vm = new ScoreboardViewModel(TestUtility.CreateExampleMatchDefinition());

            Player playerOne = vm.Player1;
            Player playerTwo = vm.Player2;

            Turn turn = vm.StartPlayerTurn();
            var turns = vm.Match.CurrentLeg.Turns;

            Assert.Single(turns);
            Assert.True(turn.PlayerId == playerOne.Id);

            turn.Tosses.Add(new Toss(10, 2));
            turn.Tosses.Add(new Toss(10, 2));
            turn.Tosses.Add(new Toss(10, 2));
            turn = vm.StartPlayerTurn();
            turns = vm.Match.CurrentLeg.Turns;

            Assert.Equal(2, turns.Count);
            Assert.True(turn.PlayerId == playerTwo.Id);
        }

        [Fact]
        public void TestComparePlayerScoreWithScoreToWinLeg()
        {
            ClearState();
            var vm = new ScoreboardViewModel(TestUtility.CreateExampleMatchDefinition());
            vm.Match.ScoreToWinLeg = 501;
            bool result = vm.ComparePlayerScoreWithScoreToWinLeg(180, new Toss(20, 3));

            Assert.False(result);

            result = vm.ComparePlayerScoreWithScoreToWinLeg(465, new Toss(18, 2));

            Assert.True(result);
        }


        [Fact]
        public void TestGetPlayerRemainders()
        {
            ClearState();

            var vm = new ScoreboardViewModel(TestUtility.CreateExampleMatchDefinition());
            vm.Match.ScoreToWinLeg = 501;

            SubmitTossInputs(vm, "t20", "t20", "t20");
            SubmitTossInputs(vm, "t20", "t10", "t5");
            SubmitTossInputs(vm, "t5", "d18", "d5");
            SubmitTossInputs(vm, "d19", "t15", "10");

            Leg leg = vm.Match.CurrentLeg;

            List<int> remainders = leg.GetRemaindersForPlayer(vm.Player1, vm.Match.ScoreToWinLeg);

            Assert.Equal(321, remainders[0]);
            Assert.Equal(260, remainders[1]);

            remainders = leg.GetRemaindersForPlayer(vm.Player2, vm.Match.ScoreToWinLeg);

            Assert.Equal(396, remainders[0]);
            Assert.Equal(303, remainders[1]);
        }

        [Fact] 
        public void TestSubmit()
        {
            ClearState();

            var vm = new ScoreboardViewModel(TestUtility.CreateExampleMatchDefinition());

            SubmitLegWinner(vm);

            Assert.Single(vm.Match.CurrentSet.Legs.Where(leg => leg.WinnerId == vm.Player1.Id));
        }

        [Fact]
        public void Test180Counter()
        {
            ClearState();

            var vm = new ScoreboardViewModel(TestUtility.CreateExampleMatchDefinition());

            SubmitLegWinner(vm);

            Assert.Equal(2, vm.Player1Counter180);
            Assert.Equal(2, vm.Player2Counter180);
        }

        [Fact]
        public void TestMatchWinner()
        {
            ClearState();

            var vm = new ScoreboardViewModel(TestUtility.CreateExampleMatchDefinition());
            vm.MessageBoxEnabled = false;

            for (int i = 0; i < 25; i++)
            {
                SubmitLegWinner(vm);
            }

            Assert.Equal(vm.Player1.Id, vm.Match.WinnerId);
        }
    }
}
