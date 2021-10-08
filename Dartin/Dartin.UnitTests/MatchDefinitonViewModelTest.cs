using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Dartin.ViewModels;
using Dartin.Models;
using Dartin;

namespace UnitTests
{
    public class MatchDefinitonViewModelTest
    {
        [Fact]
        public void AddPlayer()
        {
            State.Instance.Players.Clear();

            var vm = new MatchDefinitionViewModel(new MatchDefinition());
            vm.AddPlayer("New", "Player");
            vm.AddPlayer("Yo", "Bama");
            Assert.Equal(2 , vm.CurrentObject.Players.Count());
        }

        [Fact]
        public void SaveGameAndExit()
        {
            var vm = new MatchDefinitionViewModel(new MatchDefinition());

            vm.SelectedPlayerOne = new Player { Name = "PlayerOne" };
            vm.SelectedPlayerTwo = new Player { Name = "PlayerTwo" };

            vm.SaveGameAndExit();
            
            Assert.Equal(vm.SelectedPlayerOne, vm.CurrentObject.Players.First());
            Assert.Equal(vm.SelectedPlayerTwo, vm.CurrentObject.Players.Skip(1).First());
            Assert.Equal(2, vm.CurrentObject.Players.Count());
            //Assert.Single(vm.Matches);
        }

        [Fact]
        public void HasNoDuplicates()
        {
            var vm = new MatchDefinitionViewModel(new MatchDefinition());
            vm.AddPlayer("Yo", "Bama");
            vm.AddPlayer("Yo", "Bama");
            Assert.Single(vm.CurrentObject.Players);
        }

        [Fact]
        public void UserAddInputValidation()
        {
            var vm = new MatchDefinitionViewModel(new MatchDefinition());
            vm.AddPlayer("Yo", "Bama");
            vm.AddPlayer("タロウ", "Θεοκλεια");
            vm.AddPlayer("മലയാളം", "אַבְרָהָם");
            vm.AddPlayer("Sütterlin", "test");    

            vm.AddPlayer("test123", "test");   
            vm.AddPlayer("test", "test@@");

            Assert.True(vm.CurrentObject.Players.Count == 4);
        }
    }
}
