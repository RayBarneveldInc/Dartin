using Caliburn.Micro;
using Dartin.Models;
using System;

namespace Dartin.ViewModels
{
    public class PlayersViewModel : Screen, IViewModel
    {
        public string ViewName => throw new NotImplementedException();

        private string _searchText;
        private BindableCollection<Player> _players;
        private int _selectedIndex;

        public PlayersViewModel()
        {
            Players = new BindableCollection<Player>();

            InitializePlayers();
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

        public BindableCollection<Player> Players
        {
            get => _players;
            set
            {
                _players = value;
                NotifyOfPropertyChange(() => Players);
            }
        }

        private void InitializePlayers()
        {
            Players.Clear();

            foreach (var player in State.Instance.Players)
            {
                Players.Add(player);
            }
        }

        private void FilterPlayers(string filter)
        {
            if (string.IsNullOrEmpty(filter))
            {
                InitializePlayers();

                return;
            }

            Players.Clear();

            foreach (var player in State.Instance.Players)
            {
                if (player.Name.ToLower().Contains(filter.ToLower()))
                {
                    Players.Add(player);
                }
            }
        }

        public void Edit()
        {
            var player = Players[SelectedIndex];

            throw new NotImplementedException();
        }

        public void History()
        {
            var player = Players[SelectedIndex];

            throw new NotImplementedException();
        }

        public void Add()
        {
            throw new NotImplementedException();
        }

        public void OnExit()
        {
            throw new NotImplementedException();
        }
    }
}