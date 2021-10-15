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
        private bool _isChecked301;
        private bool _isChecked501;

        public bool IsChecked301
        {
            get => _isChecked301;
            set
            {
                _isChecked301 = OriginalMatch.ScoreToWinLeg.Equals(301) ? true : value;
                NotifyOfPropertyChange(() => IsChecked301);
            }
        }

        public bool IsChecked501
        {
            get => _isChecked501;
            set
            {
                _isChecked501 = OriginalMatch.ScoreToWinLeg.Equals(501) ? true : value;
                NotifyOfPropertyChange(() => IsChecked501);
            }
        }
        private Player _selectedPlayerOne;
        public Player SelectedPlayerOne
        {
            get => _selectedPlayerOne;
            set
            {
                _selectedPlayerOne = value;
                NotifyOfPropertyChange(() => SelectedPlayerOne);
            }
        }
        private Player _selectedPlayerTwo;
        public Player SelectedPlayerTwo
        {
            get => _selectedPlayerTwo;
            set
            {
                _selectedPlayerTwo = value;
                NotifyOfPropertyChange(() => SelectedPlayerTwo);
            }
        }
        public MatchDefinition CurrentObject { get; set; }
        public MatchDefinition OriginalMatch { get; set; }
        public BindingList<Player> Players => State.Instance.Players;

        public MatchDefinitionViewModel(MatchDefinition match)
        {
            if (!match.Players.Count.Equals(0))
            {
                if (match.Players[0] != null)
                {
                    SelectedPlayerOne = match.Players[0];
                }
                if (match.Players[1] != null)
                {
                    SelectedPlayerTwo = match.Players[1];
                }
            }

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
            if (SetMatchDefinition())
            {
                ScreenManager.GetInstance().SwitchViewModel(new MatchesViewModel(State.Instance.Matches));
            }
            else
            {
                // TODO Show error dialog OF knop disable
            }
        }

        public void SaveAndStartGame()
        {
            if (SetMatchDefinition())
            {
                ScreenManager.GetInstance().SwitchViewModel(new ScoreboardViewModel(OriginalMatch));
            }
            else
            {
                // TODO Show error dialog OF knop disable
            }

        }

        private bool SetMatchDefinition()
        {
            if (SelectedPlayerOne == SelectedPlayerTwo)
                return false;

            OriginalMatch.Date = CurrentObject.Date;
            OriginalMatch.Players.Add(SelectedPlayerOne);
            OriginalMatch.Players.Add(SelectedPlayerTwo);
            OriginalMatch.LegsToWinSet = CurrentObject.LegsToWinSet;
            OriginalMatch.SetsToWin = CurrentObject.SetsToWin;
            OriginalMatch.LegsToWinSet = CurrentObject.SetsToWin;

            if (IsChecked301)
                OriginalMatch.ScoreToWinLeg = 301;
            else if (IsChecked501)
                OriginalMatch.ScoreToWinLeg = 501;

            return true;
        }

        public void DeleteMatch()
        {
            throw new NotImplementedException(); 
        }

        public string ViewName { get; }

        public void OnExit()
        {
            throw new NotImplementedException();
        }
    }
}