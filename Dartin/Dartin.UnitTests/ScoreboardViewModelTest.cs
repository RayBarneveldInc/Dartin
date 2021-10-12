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

namespace UnitTests
{
    public class ScoreboardViewModelTest
    {
        private void ClearState()
        {
            State.Instance.Matches.Clear();
            State.Instance.Players.Clear();
        }

        [Fact]
        public void TestSetLeg()
        {
            ClearState();

            var vm = new ScoreboardViewModel();
            vm.SetLeg();

            Assert.Single(vm.Match.Sets.Last().Legs);
        }

        [Fact]
        public void TestSetSet()
        {
            ClearState();

            var vm = new ScoreboardViewModel();
            vm.SetSet();

            Assert.Equal(2, vm.Match.Sets.Count);
        }

        [Fact]
        public void TestGetLegScore()
        {
            ClearState();

            var vm = new ScoreboardViewModel();
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

            var vm = new ScoreboardViewModel();
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

        [Fact]
        public void TestStartPlayerTurn()
        {
            ClearState();

            // This test should also test for turn after set or leg is won.

            var vm = new ScoreboardViewModel();
            vm.SetLeg();

            Player playerOne = vm.Player1;
            Player playerTwo = vm.Player2;
            
            Turn turn = vm.StartPlayerTurn();
            var turns = vm.Match.Sets.Last().Legs.Last().Turns;

            Assert.Single(turns);
            Assert.True(turn.PlayerId == playerOne.Id);

            turn.Tosses.Add(new Toss(10, 2));
            turn.Tosses.Add(new Toss(10, 2));
            turn.Tosses.Add(new Toss(10, 2));
            turn = vm.StartPlayerTurn();
            turns = vm.Match.Sets.Last().Legs.Last().Turns;

            Assert.Equal(2, turns.Count);
            Assert.True(turn.PlayerId == playerTwo.Id);
        }

        [Fact]
        public void TestComparePlayerScoreWithScoreToWinLeg()
        {
            ClearState();

            var vm = new ScoreboardViewModel();
            vm.Match.Configuration.ScoreToWinLeg = 501;
            bool result = vm.ComparePlayerScoreWithScoreToWinLeg(180, new Toss(20, 3));

            Assert.False(result);

            result = vm.ComparePlayerScoreWithScoreToWinLeg(465, new Toss(18, 2));

            Assert.True(result);
        }

        [Fact]
        public void TestGetCurrentPlayerScore()
        {

        }

        [Fact]
        public void TestCalculatePlayerScoreLeft()
        {

        }

        [Fact]
        public void TestGetPlayerRemainder()
        {

        }

        [Fact]
        public void TestHandleLastTurn()
        {

        }

        [Fact] 
        public void TestSubmit()
        {

        }
    }
}
