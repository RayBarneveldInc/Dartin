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
        [Fact]
        public void FilterPlayers()
        {
            var player1 = new Player("yob", "ama");
            var player2 = new Player("ama", "yob");
            var player3 = new Player("oby", "maa");
            var player4 = new Player("boy", "aam");

            State.Instance.Players.Add(player1);
            State.Instance.Players.Add(player2);
            State.Instance.Players.Add(player3);
            State.Instance.Players.Add(player4);

            var vm = new PlayersViewModel
            {
                Players = new BindableCollection<Player>
                {
                    player1,
                    player2,
                    player3,
                    player4,
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