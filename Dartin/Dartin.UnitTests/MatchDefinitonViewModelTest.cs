using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Dartin.ViewModels;
using Dartin.Models;
using Dartin;
using System.ComponentModel;

namespace UnitTests
{
    public class MatchDefinitonViewModelTest
    {

        [Fact]
        public void SaveGameAndExit()
        {
            var vm = new MatchDefinitionViewModel();

            vm.CurrentObject = new MatchDefinition("Match name", DateTime.Now, new BindingList<Player>(), new BindingList<Set>(), new MatchConfiguration(1, 5, 501));

            vm.SelectedPlayerOne = new Player("Player", "One");
            vm.SelectedPlayerTwo = new Player("Player", "Two");

            vm.SaveGameAndExit();
            
            //Assert.Equal(vm.SelectedPlayerOne, vm.CurrentObject.Players.First());
            //Assert.Equal(vm.SelectedPlayerTwo, vm.CurrentObject.Players.Skip(1).First());
            Assert.Equal(2, vm.CurrentObject.Players.Count());
            //Assert.Single(vm.CurrentCollection);
        }
    }
}
