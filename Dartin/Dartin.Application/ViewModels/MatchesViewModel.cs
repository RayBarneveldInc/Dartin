using Caliburn.Micro;
using Dartin.Models;
using System;
using System.Text.RegularExpressions;
using Dartin.Managers;

namespace Dartin.ViewModels
{
    public class MatchesViewModel : Screen, IViewModel
    {
        private int _selectedIndex;
        private string _searchText;
        public BindableCollection<MatchDefinition> Matches { get; set; }
        public MatchesViewModel()
        {
            InitializeMatches();
        }
        private void InitializeMatches()
        {
            if (Matches == null)
            {
                Matches = new BindableCollection<MatchDefinition>(); 
            }

            Matches.Clear();

            foreach (var match in State.Instance.Matches)
            {
                Matches.Add(match);
            }
        }
        private void FilterPlayers(string filter)
        {
            if (string.IsNullOrEmpty(filter))
            {
                InitializeMatches();

                return;
            }

            Matches.Clear();

            foreach (var match in State.Instance.Matches)
            {
                if (match.Name.ToLower().Contains(filter.ToLower()))
                {
                    Matches.Add(match);
                }
            }
        }

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                FilterPlayers(_searchText);
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
        public void Edit()
        {
            var match = Matches[SelectedIndex];

            ScreenManager.GetInstance().SwitchViewModel(new MatchDefinitionViewModel(match));
        }

        public void History()
        {
            var player = Matches[SelectedIndex];

            throw new NotImplementedException();
        }

        public void Add()
        {
            ScreenManager.GetInstance().SwitchViewModel(new MatchDefinitionViewModel(new MatchDefinition()));
        }

        public void OnExit()
        {
            throw new NotImplementedException();
        }
    }
}
