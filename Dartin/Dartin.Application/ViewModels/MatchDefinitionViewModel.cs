using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Dartin.Models;
using Dartin.ViewModels;

namespace Dartin.ViewModels
{
    public class MatchDefinitionViewModel
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

        public void Exit()
        {
            Debug.WriteLine("pressed exit button");
        }

        public void SaveGameAndExit()
        {
            Debug.WriteLine("saved game and exited");
        }

        public void SaveAndStartGame()
        {
            Debug.WriteLine("saved and started game");
        }

        public void ActivateAddPlayerDialogBox()
        {
            Debug.WriteLine("activate add player dialog box");
        }
    }
}
