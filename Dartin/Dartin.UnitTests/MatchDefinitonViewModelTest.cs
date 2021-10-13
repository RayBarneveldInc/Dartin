﻿using System;
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
        private void ClearState()
        {
            State.Instance.Matches.Clear();
            State.Instance.Players.Clear();
        }

        [Fact]
        public void AddPlayer()
        {
            State.Instance.Players.Clear();
            var vm = new MatchDefinitionViewModel();
            vm.AddPlayer("New", "Player");
            vm.AddPlayer("Yo", "Bama");
            Assert.Equal(2 , vm.Players.Count());
        }

        [Fact]
        public void SaveGameAndExit()
        {
            ClearState();

            var vm = new MatchDefinitionViewModel();

            vm.CurrentObject = new MatchDefinition("Match name", DateTime.Now, new BindingList<Player>(), new BindingList<Set>(), new MatchConfiguration(1, 5, 501));

            vm.SelectedPlayerOne = new Player("PlayerOne");
            vm.SelectedPlayerTwo = new Player("PlayerTwo");

            vm.SaveGameAndExit();
            
            Assert.Equal(vm.SelectedPlayerOne, vm.CurrentObject.Players.First());
            Assert.Equal(vm.SelectedPlayerTwo, vm.CurrentObject.Players.Skip(1).First());
            Assert.Equal(2, vm.CurrentObject.Players.Count());
            Assert.Single(vm.Matches);
        }

        [Fact]
        public void HasNoDuplicates()
        {
            ClearState();

            var vm = new MatchDefinitionViewModel();
            vm.AddPlayer("Yo", "Bama");
            vm.AddPlayer("Yo", "Bama");
            Assert.Single(vm.Players);
        }

        [Fact]
        public void UserAddInputValidation()
        {
            ClearState();

            var vm = new MatchDefinitionViewModel();
            vm.AddPlayer("Yo", "Bama");
            vm.AddPlayer("タロウ", "Θεοκλεια");
            vm.AddPlayer("മലയാളം", "אַבְרָהָם");
            vm.AddPlayer("Sütterlin", "test");    

            vm.AddPlayer("test123", "test");   
            vm.AddPlayer("test", "test@@");

            Assert.True(vm.Players.Count == 4);
        }
    }
}
