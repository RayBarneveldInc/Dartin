using Caliburn.Micro;
using Dartin.Models;
using System;
using System.ComponentModel;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text.RegularExpressions;
using System.Windows;
using Dartin.Managers;
using Microsoft.Win32;
using System.Collections.Generic;

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
                    if (match.Name.Contains(filter, StringComparison.OrdinalIgnoreCase))
                    {
                        CurrentCollection.Add(match);
                    }
                }
            }
        }

        public string ViewName { get; }

        #region Buttons
        public void OnExit()
        {
            ScreenManager.GetInstance().RevertToPreviousViewModel();
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
                MessageBox.Show("No match was selected to be edited!", "Edit Match Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        public void MoreInfo()
        {
            try
            {
                MatchDefinition match = CurrentCollection[SelectedIndex];
                // Hier moet die screen van Tjeerd en Jacco
                //ScreenManager.GetInstance().SwitchViewModel(new Matches(new MatchDefinition()));
            }
            catch
            {
                MessageBox.Show("No match was selected to be edited!", "More info Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        public void Add()
        {
            ScreenManager.GetInstance().SwitchViewModel(new MatchDefinitionViewModel(State.Instance.Matches.AddNew()));
        }
        public void StartMatch()
        {
            ScreenManager.GetInstance().SwitchViewModel(new MatchDefinitionViewModel(State.Instance.Matches.AddNew()));
        }
        #endregion
    }
}
