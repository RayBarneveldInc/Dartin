using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dartin.Models;

namespace MVVM.ViewModels
{
    class MatchDefinitionViewModel
    {
        public MatchDefinition CurrentObject { get; set; }

        public MatchDefinitionViewModel(/*MatchDefinition currentMatch*/)
        {
            MatchDefinition test = new MatchDefinition();
            Player testPlayer = new Player();
            testPlayer.Name = "Tycho";
            List<Player> t = new List<Player>();
            t.Add(testPlayer);
            test.Players.Add(testPlayer);
            test.Name = "Hallo";
            CurrentObject = test;
        }
    }
}
