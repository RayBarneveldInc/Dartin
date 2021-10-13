using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Dartin.Managers;
using Dartin.Models;
using Dartin.ViewModels;
using System.Windows;
using System.Text.RegularExpressions;
using System.ComponentModel;

namespace Dartin.ViewModels
{
    public class MatchDefinitionViewModel : Screen, IViewModel

    {
        private BindableCollection<Player> _players;
        private Player _selectedPlayerOne;
        private Player _selectedPlayerTwo;
        private bool _isChecked301;
        private bool _isChecked501;

        public bool IsChecked301
        {
            get { return _isChecked301; }
            set { 
                _isChecked301 = value;
                NotifyOfPropertyChange(() => IsChecked301);
            }
        }

        public bool IsChecked501
        {
            get { return _isChecked501; }
            set { 
                _isChecked501 = value;
                NotifyOfPropertyChange(() => IsChecked501);
            }
        }

        public Player SelectedPlayerOne
        {
            get { return _selectedPlayerOne; }
            set { _selectedPlayerOne = value; }
        }

        public Player SelectedPlayerTwo
        {
            get { return _selectedPlayerTwo; }
            set { _selectedPlayerTwo = value; }
        }

        public string FirstName { get; set; }
        public string Surname { get; set; }
        public MatchDefinition CurrentObject { get; set; }
        public BindableCollection<Player> Players { get => _players; set { _players = value; NotifyOfPropertyChange(() => Players); } }
        public List<MatchDefinition> Matches { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public MatchDefinitionViewModel()
        {
            Players = new BindableCollection<Player>();
            Matches = new List<MatchDefinition>();
            CurrentObject = new MatchDefinition("Premier League Final 2017", DateTime.Today, new BindingList<Player>() { new Player("Thimo", "de Zwart"), new Player("Jasper", "van der Lugt") }, new BindingList<Set>(), new MatchConfiguration(5, 3, 501));
            FirstName = "First Name";
            Surname = "Surname";
        }

        /// <summary>
        /// Exit application.
        /// </summary>
        public void Exit()
        {
            Application.Current.Shutdown();
        }

        /// <summary>
        /// Save match and exit application.
        /// </summary>
        public void SaveGameAndExit()
        {
            CurrentObject.Players.Add(SelectedPlayerOne);
            CurrentObject.Players.Add(SelectedPlayerTwo);

            if (!IsChecked301)
            {
                CurrentObject.Configuration.ScoreToWinLeg = 301;
            } 
            else if (!IsChecked501)
            {
                CurrentObject.Configuration.ScoreToWinLeg = 501;
            }

            Matches.Add(CurrentObject);
        }

        /// <summary>
        /// Save match and start game.
        /// </summary>
        public void SaveAndStartGame()
        {
            Matches.Add(CurrentObject);
        }

        public string ViewName { get; }

        public void OnExit()
        {
            throw new NotImplementedException();
        }

        public void CreateMatch()
        {
            throw new NotImplementedException();
        }

        public int CurrentContextObject { get; set; }
    }
}