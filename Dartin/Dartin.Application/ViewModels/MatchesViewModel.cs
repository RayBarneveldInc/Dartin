using Caliburn.Micro;
using Dartin.Models;
using System;
using System.ComponentModel;
using System.Windows;
using Dartin.Managers;
using Dartin.Properties;

namespace Dartin.ViewModels
{
    public class MatchesViewModel : Screen, IViewModel
    {
        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                NotifyOfPropertyChange(() => SearchText);
                FilterMatchesOnPlayerName(_searchText);
            }
        }
        private int _selectedIndex;
        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                _selectedIndex = value;
                NotifyOfPropertyChange(() => SelectedIndex);
            }
        }
        private DateTime _selectedFilterDate;
        public DateTime SelectedFilterDate
        {
            get => _selectedFilterDate;
            set
            {
                _selectedFilterDate = value;
                NotifyOfPropertyChange(() => SelectedFilterDate);
                FilterMatchesOnDate(_selectedFilterDate);
            }
        }
        private bool _playerFilterSelected;
        public bool PlayerFilterSelected
        {
            get => _playerFilterSelected;
            set
            {
                _playerFilterSelected = value;
                NotifyOfPropertyChange(() => PlayerFilterSelected);
            }
        }
        private bool _dateFilterSelected;
        public bool DateFilterSelected
        {
            get => _dateFilterSelected;
            set
            {
                _dateFilterSelected = value;
                NotifyOfPropertyChange(() => DateFilterSelected);
                if (!_dateFilterSelected)
                {
                    CurrentCollection.Clear();
                    foreach (MatchDefinition match in OriginalCollection)
                    {
                        CurrentCollection.Add(match);
                    }
                }
            }
        }
        private BindableCollection<MatchDefinition> _currentCollection;
        public BindableCollection<MatchDefinition> CurrentCollection
        {
            get => _currentCollection;
            set
            {
                _currentCollection = value;
                NotifyOfPropertyChange(() => CurrentCollection);
            }
        }
        public BindingList<MatchDefinition> OriginalCollection { get; set; }

        public MatchesViewModel(BindingList<MatchDefinition> matches)
        {
            SelectedFilterDate = DateTime.Now.Date;
            OriginalCollection = matches;

            CurrentCollection = new BindableCollection<MatchDefinition>();
            foreach (MatchDefinition matchDefinition in OriginalCollection)
            {
                CurrentCollection.Add(matchDefinition);
            }
        }
        private void FilterMatchesOnDate(DateTime selectedFilterDate)
        {
            if (DateFilterSelected)
            {
                CurrentCollection.Clear();
                foreach (MatchDefinition match in OriginalCollection)
                {
                    if (match.Date == selectedFilterDate)
                    {
                        CurrentCollection.Add(match);
                    }
                }
            }
        }
        private void FilterMatchesOnPlayerName(string filter)
        {
            if (string.IsNullOrEmpty(filter))
            {
                CurrentCollection.Clear();
                foreach (MatchDefinition matchDefinition in OriginalCollection)
                {
                    CurrentCollection.Add(matchDefinition);
                }
            }
            else
            {
                CurrentCollection.Clear();
                foreach (MatchDefinition match in OriginalCollection)
                {
                    if (match.GetMatchName().Contains(filter, StringComparison.OrdinalIgnoreCase))
                    {
                        CurrentCollection.Add(match);
                    }
                }
            }
        }

        #region Buttons
        public void OnExit()
        {
            if (ScreenManager.GetInstance().Previous.GetType() == typeof(PlayersViewModel))
                ScreenManager.GetInstance().RevertToPreviousViewModel();
            else
                ScreenManager.GetInstance().SwitchViewModel(new MainMenuViewModel());
        }

        public void Edit()
        {
            try
            {
                MatchDefinition match = CurrentCollection[SelectedIndex];
                ScreenManager.GetInstance().SwitchViewModel(new MatchDefinitionViewModel(match));
            }
            catch
            {
                MessageBox.Show(Resources.MatchSelectedErrorText, Resources.MatchSelectedErrorTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        public void MoreInfo()
        {
            try
            {
                MatchDefinition match = CurrentCollection[SelectedIndex];
                ScreenManager.GetInstance().SwitchViewModel(new MatchReportViewModel(match));
            }
            catch
            {
                MessageBox.Show(Resources.MatchSelectedErrorText, Resources.MatchSelectedErrorTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        public void Add()
        {
            ScreenManager.GetInstance().SwitchViewModel(new MatchDefinitionViewModel(State.Instance.Matches.AddNew()));
        }
        public void StartMatch()
        {
            try
            {
                MatchDefinition match = CurrentCollection[SelectedIndex];
                ScreenManager.GetInstance().SwitchViewModel(new ScoreboardViewModel(match));
            }
            catch (IndexOutOfRangeException)
            {
                MessageBox.Show(Resources.MatchSelectedErrorText, Resources.MatchSelectedErrorTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        #endregion
    }
}
