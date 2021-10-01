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
    public class MatchDefinitionViewModel : Screen

    {
        private BindableCollection<Player> players;

        public string FirstName { get; set; }

        public string Surname { get; set; }

        public BindableCollection<Player> Players { get => players; set { players = value; NotifyOfPropertyChange(() => Players); } }

        public MatchDefinitionViewModel()
        {
            Players = new BindableCollection<Player>();
            FirstName = "First Name";
            Surname = "Surname";
        }

        public void AddPlayer(string firstName, string surname)
        {
            var fullName = firstName + " " + surname;
            if (Players.Any(p => p.Name == fullName))
            {
                return;
            }
            var newPlayer = new Player { Name = fullName};
            Players.Add(newPlayer);
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