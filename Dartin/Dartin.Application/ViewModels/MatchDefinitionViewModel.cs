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
        public Player SelectedPlayerOne { get; set; }
        public Player SelectedPlayerTwo { get; set; }

        public string FirstName { get; set; }
        public string Surname { get; set; }
        public MatchDefinition CurrentObject { get; set; }
        public MatchDefinition OriginalMatch { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public MatchDefinitionViewModel(MatchDefinition match)
        {
            CurrentObject = new MatchDefinition
            {
                Date = match.Date,
                Name = match.Name,
                SetsToWin = match.SetsToWin,
                LegsToWinSet = match.LegsToWinSet,
                ScoreToWinLeg = match.ScoreToWinLeg
            };
            OriginalMatch = match;
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
                if (CurrentObject.Players.Any(p => p.Name == fullName))
                {
                    return;
                }
                var newPlayer = new Player { Name = fullName };
                CurrentObject.Players.Add(newPlayer);

                State.Instance.Players.Clear();
                foreach (var player in CurrentObject.Players)
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

            State.Instance.Matches.Add(CurrentObject);
        }

        /// <summary>
        /// Save match and start game.
        /// </summary>
        public void SaveAndStartGame()
        {
            if (OriginalMatch.Equals(null))
                State.Instance.Matches.Add(CurrentObject);
            else
                OriginalMatch = CurrentObject;
        }

        public string ViewName { get; }

        public void OnExit()
        {
            throw new NotImplementedException();
        }

        public int CurrentContextObject { get; set; }
    }
}