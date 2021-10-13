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
            BindingList<MatchDefinition> CurrentCollection = new BindingList<MatchDefinition>
            {
                new() {Name = "yob ama"},
                new() {Name = "ama yob"},
                new() {Name = "oby maa"},
                new() {Name = "boy aam"},
            };

            var vm = new MatchesViewModel(CurrentCollection)
            {
                SearchText = "yob"
            };

            Assert.Equal(2, vm.CurrentCollection.Count);
        }
    }
}
