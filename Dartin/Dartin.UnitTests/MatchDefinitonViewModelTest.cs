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
        public void HasNoDuplicates()
        {
            var vm = new MatchDefinitionViewModel();
            vm.AddPlayer("Yo", "Bama");
            vm.AddPlayer("Yo", "Bama");
            Assert.Single(vm.Players);
        }
    }
}
