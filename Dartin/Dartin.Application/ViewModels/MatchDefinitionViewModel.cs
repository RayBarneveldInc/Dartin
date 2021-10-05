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

namespace Dartin.ViewModels
{
    public class MatchDefinitionViewModel : Screen, IViewModel

    {
        private BindableCollection<Player> _players;
        private Player _selectedPlayerOne;
        private Player _selectedPlayerTwo;

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
            CurrentObject = new MatchDefinition
            {
                Date = DateTime.Now,
                Name = "Match name",
                SetsToWin = 1,
                LegsToWinSet = 5,
                ScoreToWinLeg = 501
            };
            FirstName = "First Name";
            Surname = "Surname";
        }

        /// <summary>
        /// Add player to list.
        /// </summary>
        /// <param name="firstName">string</param>
        /// <param name="surname">string</param>
        public void AddPlayer(string firstName, string surname)
        {
            var fullName = firstName + " " + surname;
            var match = Regex.Match(fullName, @"^[\p{L}\p{M}' \.\-]+$", RegexOptions.IgnoreCase);

            if (match.Success)
            {
                if (Players.Any(p => p.Name == fullName))
                {
                    return;
                }
                var newPlayer = new Player { Name = fullName };
                Players.Add(newPlayer);

                State.Instance.Players.Clear();
                foreach (var player in Players)
                {
                    State.Instance.Players.Add(player);
                }
            }
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

        }

        public int CurrentContextObject { get; set; }
    }
}