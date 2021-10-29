using Caliburn.Micro;
using Dartin.Models;
using System;
using System.Linq;
using System.Windows;
using Dartin.Managers;
using Action = System.Action;
using System.ComponentModel;
using Dartin.Properties;
using FamFamFam.Flags.Wpf;

namespace Dartin.ViewModels
{
    public class PlayersViewModel : Screen, IViewModel
    {
        private string _searchText;
        private BindableCollection<Player> _players;
        private int _selectedIndex;
        private Visibility _crudModalVisibility;
        private Visibility _deleteModalVisibility;

        private string _lastName;
        private string _firstName;
        private CountryData _nationality;
        private string _modalButtonText;
        private string _title;

        public Action PlayerAction { get; set; }
        public Action DeleteAction { get; set; }

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

        public CountryData Nationality
        {
            get => _nationality;
            set
            {
                _nationality = value;
                NotifyOfPropertyChange(() => Nationality);
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

        public Visibility DeleteModalVisibility
        {
            get => _deleteModalVisibility;
            set
            {
                _deleteModalVisibility = value;
                NotifyOfPropertyChange(() => DeleteModalVisibility);
            }
        }


        public PlayersViewModel()
        {
            Players = new BindableCollection<Player>();

            CrudModalVisibility = Visibility.Hidden;
            DeleteModalVisibility = Visibility.Hidden;


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

            Title = Resources.EditPlayerTitle;
            ModalButtonText = Resources.EditPlayerButton;

            FirstName = selectedPlayer.FirstName;
            LastName = selectedPlayer.LastName;
            Nationality = CountryData.AllCountries.Where(nationality => nationality.Iso2 == selectedPlayer.Nationality).First();

            PlayerAction = () =>
            {
                var playerFromState = State.Instance.Players.FirstOrDefault(x => x.Id == selectedPlayer.Id);

                playerFromState.FirstName = FirstName;
                playerFromState.LastName = LastName;
                playerFromState.Nationality = Nationality.Iso2;
            };

            ToggleModal();
        }

        public void Add()
        {
            Title = Resources.AddPlayerTitle;
            ModalButtonText = Resources.AddPlayerTitle;

            FirstName = string.Empty;
            LastName = string.Empty;
            Nationality = CountryData.AllCountries.First();

            PlayerAction = () =>
            {
                State.Instance.Players.Add(new Player(FirstName, LastName, Nationality.Iso2));
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


        public void ConfirmDelete()
        {
            DeleteAction.Invoke();

            DeleteModalVisibility = ~DeleteModalVisibility;
        }

        public void CancelDelete()
        {
            DeleteModalVisibility = ~DeleteModalVisibility;
        }

        public void Delete()
        {
            if (SelectedIndex < 0 || SelectedIndex >= Players.Count)
            {
                return;
            }

            var player = Players[SelectedIndex];

            DeleteAction = () =>
            {

                State.Instance.Players.Remove(player);

                var newMatchList = new BindingList<MatchDefinition>();

                foreach (var match in State.Instance.Matches)
                {
                    if (!match.Players.Contains(player.Id))
                    {
                        newMatchList.Add(match);
                    }
                }

                State.Instance.Matches = newMatchList;

                InitializePlayers();

            };

            DeleteModalVisibility = ~DeleteModalVisibility;

        }

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