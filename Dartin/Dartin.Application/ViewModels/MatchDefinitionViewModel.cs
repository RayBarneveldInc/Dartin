using Caliburn.Micro;
using Dartin.Managers;
using Dartin.Models;
using Dartin.Properties;
using System;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;

namespace Dartin.ViewModels
{
    public class MatchDefinitionViewModel : Screen, IViewModel
    {
        private bool _isChecked501;
        public bool IsChecked501
        {
            get => _isChecked501;
            set
            {
                _isChecked501 = value;
                NotifyOfPropertyChange(() => IsChecked501);
            }
        }
        private bool _isChecked301;
        public bool IsChecked301
        {
            get => _isChecked301;
            set
            {
                _isChecked301 = value;
                NotifyOfPropertyChange(() => IsChecked301);
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
            CurrentObject = new MatchDefinition
            {
                Date = match.Date,
                SetsToWin = match.SetsToWin,
                LegsToWinSet = match.LegsToWinSet,
                ScoreToWinLeg = match.ScoreToWinLeg
            };

            OriginalMatch = match;

            if (!match.Players.Count().Equals(0))
            {
                foreach (Player p in Players)
                {
                    if (match.Players[0].Id == p.Id)
                    {
                        SelectedPlayerOne = p;
                    }
                    if (match.Players[1].Id == p.Id)
                    {
                        SelectedPlayerTwo = p;
                    }
                }
            }
            if (match.ScoreToWinLeg == 501 || match.ScoreToWinLeg == 0)
            {
                IsChecked501 = true;
                IsChecked301 = false;
            }
            else
            {
                IsChecked501 = false;
                IsChecked301 = true;
            }
        }

        public void SaveAndStartGame()
        {
            switch (checkMatchDefinition())
            {
                case ErrorDialogEnum.Ok:
                    SetMatchDefinition();
                    ScreenManager.GetInstance().SwitchViewModel(new ScoreboardViewModel(OriginalMatch));
                    break;
                case ErrorDialogEnum.SelectedPlayersAreEqual:
                    MessageBox.Show(Resources.DuplicatePlayersSelected, Resources.MatchWarningTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                    break;
                case ErrorDialogEnum.NotAllPlayersAreSelected:
                    MessageBox.Show(Resources.OnePlayerIsNull, Resources.MatchWarningTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                    break;
            }
        }
        private enum ErrorDialogEnum
        {
            Ok,
            NotAllPlayersAreSelected,
            SelectedPlayersAreEqual
        };
        private ErrorDialogEnum checkMatchDefinition()
        {
            if (SelectedPlayerOne == null)
                return ErrorDialogEnum.NotAllPlayersAreSelected;
            if (SelectedPlayerTwo == null)
                return ErrorDialogEnum.NotAllPlayersAreSelected;
            if (SelectedPlayerOne == SelectedPlayerTwo)
                return ErrorDialogEnum.SelectedPlayersAreEqual;
            return ErrorDialogEnum.Ok;
        }
        private void SetMatchDefinition()
        {
            if (OriginalMatch.Players.Count.Equals(0))
            {
                OriginalMatch.Players.Add(SelectedPlayerOne);
                OriginalMatch.Players.Add(SelectedPlayerTwo);
            }
            else
            {
                OriginalMatch.Players[0] = SelectedPlayerOne;
                OriginalMatch.Players[1] = SelectedPlayerTwo;
            }

            OriginalMatch.Date = CurrentObject.Date;
            OriginalMatch.LegsToWinSet = CurrentObject.LegsToWinSet;
            OriginalMatch.SetsToWin = CurrentObject.SetsToWin;

            if (IsChecked501)
                OriginalMatch.ScoreToWinLeg = 501;
            else
                OriginalMatch.ScoreToWinLeg = 301;
        }

        public void DeleteMatch()
        {
            if (MessageBox.Show(Resources.DeleteMatchWarningMessage, Resources.MatchWarningTitle, MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                State.Instance.Matches.Remove(OriginalMatch);
                ScreenManager.GetInstance().SwitchViewModel(new MatchesViewModel(State.Instance.Matches));
            }
        }

        public string ViewName { get; }

        public void OnExit()
        {
            switch (checkMatchDefinition())
            {
                case ErrorDialogEnum.Ok:
                    SetMatchDefinition();
                    ScreenManager.GetInstance().SwitchViewModel(new MatchesViewModel(State.Instance.Matches));
                    break;
                case ErrorDialogEnum.SelectedPlayersAreEqual:
                    MessageBox.Show(Resources.DuplicatePlayersSelected, Resources.MatchWarningTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                    break;
                case ErrorDialogEnum.NotAllPlayersAreSelected:
                    MessageBox.Show(Resources.OnePlayerIsNull, Resources.MatchWarningTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                    break;
            }
        }
    }
}