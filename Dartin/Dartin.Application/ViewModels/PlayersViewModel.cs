using Caliburn.Micro;
using Dartin.Models;
using System;
using System.Linq;
using System.Windows;
using Dartin.Managers;
using Action = System.Action;

namespace Dartin.ViewModels
{
    public class PlayersViewModel : Screen, IViewModel
    {
        public string ViewName => throw new NotImplementedException();

        private string _searchText;
        private BindableCollection<Player> _players;
        private int _selectedIndex;
        private Visibility _crudModalVisibility;
        private string _lastName;
        private string _firstName;
        private string _modalButtonText;
        private string _title;

        // CrudModel
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                NotifyOfPropertyChange(() => Title);
            }
        }

        public Action PlayerAction { get; set; }

        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                NotifyOfPropertyChange(() => FirstName);
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                NotifyOfPropertyChange(() => LastName);
            }
        }

        public string ModalButtonText
        {
            get => _modalButtonText;
            set
            {
                _modalButtonText = value;
                NotifyOfPropertyChange(() => ModalButtonText);
            }
        }

        public Visibility CrudModalVisibility
        {
            get => _crudModalVisibility;
            set
            {
                _crudModalVisibility = value;
                NotifyOfPropertyChange(() => CrudModalVisibility);
            }
        }


        public PlayersViewModel()
        {
            Players = new BindableCollection<Player>();

            CrudModalVisibility = Visibility.Hidden;

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
                if (player.Name.Contains(filter, StringComparison.OrdinalIgnoreCase))
                {
                    Players.Add(player);
                }
            }
        }

        public void Edit()
        {
            if (SelectedIndex < 0 || SelectedIndex >= Players.Count)
            {
                return;
            }
            
            var selectedPlayer = Players[SelectedIndex];

            Title = "Edit Player";
            ModalButtonText = "Edit";

            FirstName = selectedPlayer.FirstName;
            LastName = selectedPlayer.LastName;

            PlayerAction = () =>
            {
                var playerFromState = State.Instance.Players.FirstOrDefault(x => x.Id == selectedPlayer.Id);

                playerFromState.FirstName = FirstName;
                playerFromState.LastName = LastName;
            };
            
            ToggleModal();
        }

        public void Add()
        {
            Title = "Add Player";
            ModalButtonText = "Add";

            FirstName = string.Empty;
            LastName = string.Empty;

            PlayerAction = () =>
            {
                State.Instance.Players.Add(new Player(FirstName,LastName));
            };
            
            ToggleModal();
        }

        public void EditAddButtonClick()
        {
            PlayerAction.Invoke();

            ToggleModal();

            InitializePlayers();
        }

        public void ToggleModal() => CrudModalVisibility = ~CrudModalVisibility;


        public void History()
        {
            if (SelectedIndex < 0 || SelectedIndex >= Players.Count)
            {
                return;
            }
            
            var player = Players[SelectedIndex];

            ScreenManager.GetInstance().SwitchViewModel(new MatchesViewModel(State.Instance.Matches)
            {
                PlayerFilterSelected = true,
                SearchText = player.Name
            });
        }

        public void Back()
        {
            ScreenManager.GetInstance().SwitchViewModel(new MainMenuViewModel());
        }

        public void OnExit()
        {
            throw new NotImplementedException();
        }
    }
}