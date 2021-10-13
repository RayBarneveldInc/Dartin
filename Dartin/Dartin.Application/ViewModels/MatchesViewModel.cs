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

namespace Dartin.ViewModels
{
    public class MatchesViewModel : Screen, IViewModel
    {
        private int _selectedIndex;
        private string _searchText;
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
            OriginalCollection = matches;

            CurrentCollection = new BindableCollection<MatchDefinition>();
            foreach (MatchDefinition matchDefinition in OriginalCollection)
            { 
                CurrentCollection.Add(matchDefinition);
            }
        }

        private void FilterMatches(string filter)
        {
            if (string.IsNullOrEmpty(filter))
            {
                CurrentCollection = new BindableCollection<MatchDefinition>();
                foreach (MatchDefinition matchDefinition in OriginalCollection)
                {
                    CurrentCollection.Add(matchDefinition);
                }
            }
            else
            {
                CurrentCollection = OriginalCollection.Where(match => match.Name.Contains(filter, StringComparison.OrdinalIgnoreCase)) as BindableCollection<MatchDefinition>;
            }
        }

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                FilterMatches(_searchText);
            }
        }

        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                _selectedIndex = value;
                NotifyOfPropertyChange(() => SelectedIndex);
            }
        }

        public string ViewName { get; }

        public void OnExit()
        {
            throw new NotImplementedException();
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
            MatchDefinition match = CurrentCollection[SelectedIndex];

            // Hier moet die screen van Tjeerd en Jacco
            //ScreenManager.GetInstance().SwitchViewModel(new Matches(new MatchDefinition()));
        }

        public void Add()
        {
            ScreenManager.GetInstance().SwitchViewModel(new MatchDefinitionViewModel(State.Instance.Matches.AddNew()));
        }
        public void StartMatch()
        {
            ScreenManager.GetInstance().SwitchViewModel(new MatchDefinitionViewModel(State.Instance.Matches.AddNew()));
        }

    }
}
