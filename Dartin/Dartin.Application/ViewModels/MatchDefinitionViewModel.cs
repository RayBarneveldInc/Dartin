using Caliburn.Micro;
using Dartin.Managers;
using Dartin.Models;
using System;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;

namespace Dartin.ViewModels
{
    public class MatchDefinitionViewModel : Screen, IViewModel
    {
        public Player SelectedPlayerOne
        {
            get
            {
                try
                {
                    return CurrentObject.Players[0];
                }
                catch
                {
                    return null;
                }
            }
            set
            {
                if (value != null)
                {
                    CurrentObject.Players.Add(value);
                }
            }
        }

        public Player SelectedPlayerTwo
        {
            get
            {
                try
                {
                    return CurrentObject.Players[1];
                }
                catch
                {
                    return null;
                }
            }
            set
            {
                if (value != null)
                {
                    CurrentObject.Players.Add(value);
                }
            }
        }

        public string FirstName { get; set; }
        public string Surname { get; set; }
        public MatchDefinition CurrentObject { get; set; }
        public MatchDefinition OriginalMatch { get; set; }

        public BindingList<Player> Players => State.Instance.Players;

        public MatchDefinitionViewModel(MatchDefinition match)
        {
            CurrentObject = new MatchDefinition
            {
                Date = match.Date,
                SetsToWin = match.SetsToWin,
                LegsToWinSet = match.LegsToWinSet,
                ScoreToWinLeg = match.ScoreToWinLeg
            };
            OriginalMatch = match;
        }

        public void Exit()
        {
            ScreenManager.GetInstance().SwitchViewModel(new MatchesViewModel(State.Instance.Matches));
        }

        public void SaveGameAndExit()
        {
            OriginalMatch.Date = CurrentObject.Date;
            OriginalMatch.Players = CurrentObject.Players;
            OriginalMatch.LegsToWinSet = CurrentObject.LegsToWinSet;
            OriginalMatch.SetsToWin = CurrentObject.SetsToWin;
            OriginalMatch.LegsToWinSet = CurrentObject.SetsToWin;

            ScreenManager.GetInstance().SwitchViewModel(new MatchesViewModel(State.Instance.Matches));
        }

        public void SaveAndStartGame()
        {
            // Hier moet dan het spel gestart worden
            //if (OriginalMatch.Equals(null))
            //    State.Instance.Matches.Add(CurrentObject);
            //else
            //    OriginalMatch = CurrentObject;

        }

        public string ViewName { get; }

        public void OnExit()
        {
            throw new NotImplementedException();
        }

        public int CurrentContextObject { get; set; }
    }
}