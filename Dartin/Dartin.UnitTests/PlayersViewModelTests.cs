using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Dartin;
using Dartin.Models;
using Dartin.ViewModels;
using Xunit;

namespace UnitTests
{
    public class PlayersViewModelTests
    {
        public void FilterPlayers()
        {
            var vm = new PlayersViewModel
            {
                Players = new BindableCollection<Player>
                {
                    new Player("yob ama"),
                    new Player("ama yob"),
                    new Player("oby maa"),
                    new Player("boy aam"),
                },
                SearchText = "yob"
            };

            Assert.Equal(2, vm.Players.Count);
        }

        [Fact]
        public void AddPlayer()
        {
            var vm = new PlayersViewModel();
            
            State.Instance.Players.Clear();
            
            vm.Players = new BindableCollection<Player>();

            vm.Add();
            
            vm.FirstName = "newbama";
            
            vm.LastName = "obama";
            
            vm.EditAddButtonClick();
            
            Assert.Single(vm.Players);
        }
    }
}