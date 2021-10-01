using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Dartin.ViewModels;
using Dartin.Models;

namespace UnitTests
{
    public class MatchDefinitonViewModelTest
    {
        [Fact]
        public void AddPlayer()
        {
            var vm = new MatchDefinitionViewModel();
            vm.AddPlayer("New", "Player");
            vm.AddPlayer("Yo", "Bama");
            Assert.Equal(2 , vm.Players.Count());
        }

        [Fact]
        public void SaveGameAndExit()
        {
            var vm = new MatchDefinitionViewModel();

            vm.CurrentObject = new MatchDefinition
            {
                Date = DateTime.Now,
                Name = "Match name",
                SetsToWin = 1,
                LegsToWinSet = 5,
                ScoreToWinLeg = 501
            };

            vm.SelectedPlayerOne = new Player { Name = "PlayerOne" };
            vm.SelectedPlayerTwo = new Player { Name = "PlayerTwo" };

            vm.SaveGameAndExit();
            
            Assert.Equal(vm.SelectedPlayerOne, vm.CurrentObject.Players.First());
            Assert.Equal(vm.SelectedPlayerTwo, vm.CurrentObject.Players.Skip(1).First());
            Assert.Equal(2, vm.CurrentObject.Players.Count());
            Assert.Single(vm.Matches);
        }

        [Fact]
        public void HasNoDuplicates()
        {
            var vm = new MatchDefinitionViewModel();
            vm.AddPlayer("Yo", "Bama");
            vm.AddPlayer("Yo", "Bama");
            Assert.Single(vm.Players);
        }
    }
}
