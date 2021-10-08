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
    class MatchesViewModelTests
    {
        public void FilterMatches()
        {
            var vm = new MatchesViewModel()
            {
                Matches = new BindableCollection<MatchDefinition>
                {
                    new() {Name = "yob ama"},
                    new() {Name = "ama yob"},
                    new() {Name = "oby maa"},
                    new() {Name = "boy aam"},
                },
                SearchText = "yob"
            };

            Assert.Equal(2, vm.Matches.Count);
        }
    }
}
