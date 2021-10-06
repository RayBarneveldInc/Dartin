using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
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
            
            Assert.Equal(2,vm.Players.Count);
        }
    }
}
