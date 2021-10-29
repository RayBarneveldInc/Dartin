using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Dartin.Models;
using Dartin.ViewModels;
using Xunit;

namespace UnitTests
{
    class MatchesViewModelTests
    {
        public void FilterMatches()
        {
            BindingList<MatchDefinition> CurrentCollection = new BindingList<MatchDefinition>();

            for (int i = 0; i < 5; i++)
            {
                MatchDefinition matchDefinition = new MatchDefinition();
                Player player1 = new Player
                {
                    FirstName = "Maurice",
                    LastName = "Ponte"
                };

                Player player2 = new Player
                {
                    FirstName = "Jacco",
                    LastName = "Blok"
                };

                matchDefinition.Players.Add(player1.Id);
                matchDefinition.Players.Add(player2.Id);
            }

            for (int i = 0; i < 5; i++)
            {
                MatchDefinition matchDefinition = new MatchDefinition();
                Player player1 = new Player
                {
                    FirstName = "Thimo",
                    LastName = "de Zwart"
                };

                Player player2 = new Player
                {
                    FirstName = "Boele",
                    LastName = "Boom"
                };

                matchDefinition.Players.Add(player1.Id);
                matchDefinition.Players.Add(player2.Id);
            }


            var vm = new MatchesViewModel(CurrentCollection)
            {
                SearchText = "Maurice"
            };

            Assert.Equal(5, vm.CurrentCollection.Count);
        }
    }
}
