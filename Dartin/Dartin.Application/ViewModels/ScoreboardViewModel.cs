using Caliburn.Micro;
using Dartin.Abstracts;
using Dartin.Managers;
using Dartin.Models;
using Dartin.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Parser = Dartin.Managers.Parser;
using System.Diagnostics;
using System.Windows;

namespace Dartin.ViewModels
{
    class ScoreboardViewModel : Screen, IViewModel
    {
        private Leg _currentLeg;
        private Set _currentSet;
        private int _player1LegScore;
        private int _player2LegScore;
        private int _player1SetScore;
        private int _player2SetScore;
        private string _tossOneInput;
        private string _tossTwoInput;
        private string _tossThreeInput;
        private string _legText;
        private string _setText;
        private Visibility _playerOneTurnIndicatorIsVisible;
        private Visibility _playerTwoTurnIndicatorIsVisible;
        private BindableCollection<Turn> _player1Turns;
        private BindableCollection<Turn> _player2Turns;
        private BindableCollection<int> _player1Remainders;
        private BindableCollection<int> _player2Remainders;


        public string TossOneInput
        {
            get
            {
                return _tossOneInput;
            }
            set
            {
                _tossOneInput = value;
                NotifyOfPropertyChange(() => TossOneInput);
            }
        }

        public string TossTwoInput
        {
            get
            {
                return _tossTwoInput;
            }
            set
            {
                _tossTwoInput = value;
                NotifyOfPropertyChange(() => TossTwoInput);
            }
        }

        public string TossThreeInput
        {
            get
            {
                return _tossThreeInput;
            }
            set
            {
                _tossThreeInput = value;
                NotifyOfPropertyChange(() => TossThreeInput);
            }
        }

        public Visibility PlayerOneTurnIndicatorIsVisible
        {
            get => _playerOneTurnIndicatorIsVisible;
            set
            {
                _playerOneTurnIndicatorIsVisible = value;
                NotifyOfPropertyChange(() => PlayerOneTurnIndicatorIsVisible);
            }
        }

        public Visibility PlayerTwoTurnIndicatorIsVisible
        {
            get => _playerTwoTurnIndicatorIsVisible;
            set
            {
                _playerTwoTurnIndicatorIsVisible = value;
                NotifyOfPropertyChange(() => PlayerTwoTurnIndicatorIsVisible);
            }
        }

        public string ViewName => nameof(ScoreboardViewModel);
        public Player Player1 => Match.Players.First();
        public Player Player2 => Match.Players[1];
        public BindableCollection<Turn> Player1Turns
        {
            get => _player1Turns;
            set
            {
                _player1Turns = value;
                NotifyOfPropertyChange(() => Player1Turns);
            }
        }
        public BindableCollection<Turn> Player2Turns
        {
            get => _player2Turns;
            set
            {
                _player2Turns = value;
                NotifyOfPropertyChange(() => Player2Turns);
            }
        }

        public int Player1LegScore
        {
            get => _player1LegScore;
            set
            {
                _player1LegScore = value;
                NotifyOfPropertyChange(() => Player1LegScore);
            }
        }

        public int Player2LegScore
        {
            get
            {
                return _player2LegScore;
            }
            set
            {
                _player2LegScore = value;
                NotifyOfPropertyChange(() => Player2LegScore);
            }
        }

        public int Player1SetScore
        {
            get => _player1SetScore;
            set
            {
                _player1SetScore = value;
                NotifyOfPropertyChange(() => Player1SetScore);
            }
        }

        public int Player2SetScore
        {
            get
            {
                return _player2SetScore;
            }
            set
            {
                _player2SetScore = value;
                NotifyOfPropertyChange(() => Player2SetScore);
            }
        }

        public BindableCollection<int> Player1Remainders
        {
            get => _player1Remainders;
            set
            {
                _player1Remainders = value;
                NotifyOfPropertyChange(() => Player1Remainders);
            }
        }
        public BindableCollection<int> Player2Remainders
        {
            get => _player2Remainders;
            set
            {
                _player2Remainders = value;
                NotifyOfPropertyChange(() => Player2Remainders);
            }
        }
        public string BestOf => $"Best of {Match.Configuration.SetsToWin} sets ({Match.Configuration.LegsToWinSet} legs per set)";
        public MatchDefinition Match { get; }
        public string LegText
        {
            get
            {
                return _legText;
            }
            set
            {
                _legText = value;
                NotifyOfPropertyChange(() => LegText);
            }
        }

        public string SetText
        {
            get
            {
                return _setText;
            }
            set
            {
                _setText = value;
                NotifyOfPropertyChange(() => SetText);
            }
        }

        public ScoreboardViewModel()
        {
            State.Instance.Players.Add(new Player("Thimo de Zwart"));
            State.Instance.Players.Add(new Player("Jasper van der Lugt"));
            Match = new MatchDefinition("Premier League Final 2017", DateTime.Today, new BindingList<Player>() { State.Instance.Players[0], State.Instance.Players[1] }, new BindingList<Set>(), new MatchConfiguration(5, 3, 501));
            SetSet();
            State.Instance.Matches.Add(Match);
            TogglePlayerTurnIndicator(true);
            SetSetText();
            SetLegText();
        }

        public void OnExit()
        {
            throw new System.NotImplementedException();
        }

        public void SetLeg()
        {
            _currentLeg = new Leg(new BindingList<Turn>());
            Match.Sets.Last().Legs.Add(_currentLeg);
        }
        
        public void SetSet()
        {
            _currentSet = new Set(new BindingList<Leg>());
            Match.Sets.Add(_currentSet);
        }

        public void SetScores()
        {
            Player1LegScore = GetLegScore(Player1);
            Player1SetScore = GetSetScore(Player1);
            Player2LegScore = GetLegScore(Player2);
            Player2SetScore = GetSetScore(Player2);
        }

        private int GetLegScore(Player player)
        {
            return Match.Sets.Any() && Match.Sets.Last().Legs.Any() ? Match.Sets.Last().Legs.Count(leg => leg.WinnerId == player.Id) : 0;
        }
        
        private int GetSetScore(Player player)
        {
            return Match.Sets.Any() ? Match.Sets.Count(set => set.WinnerId == player.Id) : 0;
        }

        public void SetLegText()
        {
            int legCount;

            if (Match.Sets.Any() && Match.Sets.Last().Legs.Any())
            {
                legCount = Match.Sets.Last().Legs.Count;
            }
            else
            {
                legCount = 1;
            }

            LegText = $"Leg {legCount}";
        }

        public void SetSetText()
        {
            int setCount;

            if (Match.Sets.Any())
            {
                setCount = Match.Sets.Count;
            }
            else
            {
                setCount = 1;
            }

            SetText = $"Set {setCount}";
        }

        public Player StartPlayerTurn()
        {
            if (!_currentLeg.Turns.Any() || !_currentLeg.Turns.Last().Valid || _currentLeg.Turns.Last().WinningTurn || _currentLeg.Turns.Last().Tosses.Count >= 3)
            {
                if (_currentLeg.Turns.Count % 2 == 0)
                {
                    _currentLeg.Turns.Add(new Turn(Player1, new BindingList<Toss>()));
                    TogglePlayerTurnIndicator(false);
                    var remainders = _currentLeg.GetRemaindersForPlayer(Player1, Match.Configuration.ScoreToWinLeg);
                }
                else
                {
                    _currentLeg.Turns.Add(new Turn(Player2, new BindingList<Toss>()));
                    TogglePlayerTurnIndicator();
                    var remainders = _currentLeg.GetRemaindersForPlayer(Player2, Match.Configuration.ScoreToWinLeg);
                }
            }

            return _currentLeg.Turns.Last().PlayerId.ToPlayer();
        }

        public void TogglePlayerTurnIndicator(bool playerOneActive = true)
        {
            if (playerOneActive)
            {
                PlayerOneTurnIndicatorIsVisible = Visibility.Visible;
                PlayerTwoTurnIndicatorIsVisible = Visibility.Hidden;
            }
            else
            {
                PlayerOneTurnIndicatorIsVisible = Visibility.Hidden;
                PlayerTwoTurnIndicatorIsVisible = Visibility.Visible;
            }
        }

        public void ProcessTossInputTurn(string tossInput)
        {
            if (!string.IsNullOrEmpty(tossInput) && Parser.ParseThrow(tossInput) != null)
            {
                Toss toss = Parser.ParseThrow(tossInput);
                Turn currentTurn = GetCurrentTurn();
                Player activePlayer = GetActivePlayer();
                int currentPlayerScore = GetCurrentPlayerScore();

                if (currentPlayerScore + toss.TotalScore < Match.Configuration.ScoreToWinLeg)
                {
                    currentTurn.Tosses.Add(toss);
                }
                else if (ComparePlayerScoreWithScoreToWinLeg(currentPlayerScore, toss) && toss.Multiplier == 2)
                {
                    currentTurn.Tosses.Add(toss);
                    currentTurn.WinningTurn = true;
                    _currentLeg.WinnerId = activePlayer.Id;
                }
                else if (
                    (ComparePlayerScoreWithScoreToWinLeg(currentPlayerScore, toss) && toss.Multiplier != 2) ||
                    (currentPlayerScore + toss.TotalScore) > Match.Configuration.ScoreToWinLeg)
                {
                    currentTurn.Tosses.Add(toss);
                    currentTurn.Valid = false;
                }

                var wonLegs = Match.Sets.Last().Legs.Count(leg => leg.WinnerId == activePlayer.Id);

                if (Match.Configuration.LegsToWinSet == wonLegs)
                {
                    _currentSet.WinnerId = activePlayer.Id;
                }
            }
        }

        public bool ComparePlayerScoreWithScoreToWinLeg(int currentPlayerScore, Toss toss)
        {
            return (currentPlayerScore + toss.TotalScore) == Match.Configuration.ScoreToWinLeg;
        }

        public Turn GetCurrentTurn()
        {
            return _currentLeg.Turns.Last();
        }

        public Player GetActivePlayer()
        {
            return GetCurrentTurn().PlayerId.ToPlayer();
        }

        public void ClearScoreListViews()
        {
            Player1Turns.Clear();
            Player2Turns.Clear();
            Player1Remainders.Clear();
            Player2Remainders.Clear();
        }

        public int GetCurrentPlayerScore()
        {
            Player activePlayer = GetActivePlayer();

            return _currentLeg.Turns.Where(turn => turn.PlayerId == activePlayer.Id && turn.Valid).Sum(turn => turn.Score);
        }

        public int CalculatePlayerScoreLeft()
        {
            return Match.Configuration.ScoreToWinLeg - _currentLeg.Turns.Where(turn => turn.PlayerId == _currentLeg.Turns.Last().PlayerId && turn.Valid).Sum(turn => turn.Score);
        }

        public BindableCollection<Turn> GetPlayerTurnsCollection(Player player)
        {
            return new BindableCollection<Turn>(Match.Sets.Last().Legs.Last().Turns.Where(turn => turn.PlayerId == player.Id && turn.Valid));
        }

        public int GetPlayerRemainder(BindableCollection<Turn> turns)
        {
            return Match.Configuration.ScoreToWinLeg - turns.Sum(turn => turn.Score);
        }

        public void RevertTurn()
        {
            if (_currentLeg.Turns.Any())
            {
                var player = GetActivePlayer();
                _currentLeg.Turns.RemoveLast();
                if (player == Player1)
                {
                    Player1Remainders.RemoveLast();
                    Player1Turns.RemoveLast();
                }
                else
                {
                    Player2Remainders.RemoveLast();
                    Player2Turns.RemoveLast();
                }
                TogglePlayerTurnIndicator(player == Player1);
            }
        }

        public void HandlePlayerScore()
        {
            Player activePlayer = GetActivePlayer();

            if (activePlayer.Id == Player1.Id)
            {
                Player1Turns = GetPlayerTurnsCollection(Player1);
            }
            else
            {
                Player2Turns = GetPlayerTurnsCollection(Player2);
            }
        }

        public void HandleEndTurn()
        {
            Turn currentTurn = GetCurrentTurn();
            Player activePlayer = GetActivePlayer();

            if (!currentTurn.Valid)
            {
                Debug.WriteLine($"{activePlayer.Name} scores 0 points!");
            }
            else if (!currentTurn.WinningTurn && currentTurn.Tosses.Count == 3)
            {
                Debug.WriteLine($"{activePlayer.Name} scores {currentTurn.Score} points!");
            }
            else if (currentTurn.WinningTurn)
            {
                ClearScoreListViews();

                if (_currentSet.WinnerId == activePlayer.Id)
                {
                    SetSet();

                    Debug.WriteLine($"{activePlayer.Name} wins the set!");
                }
                else
                {
                    SetLeg();

                    Debug.WriteLine($"{activePlayer.Name} wins the leg!");
                }

                SetSetText();
                SetLegText();
                SetScores();
            }

            if (Match.Sets.Count(set => set.WinnerId == activePlayer.Id) == Match.Configuration.SetsToWin)
            {
                Debug.WriteLine($"{activePlayer.Name} wins the match!");
            }

            if (activePlayer == Player1)
            {
                Player1Remainders = new BindableCollection<int>(_currentLeg.GetRemaindersForPlayer(Player1, Match.Configuration.ScoreToWinLeg));
            }
            else
            {
                Player2Remainders = new BindableCollection<int>(_currentLeg.GetRemaindersForPlayer(Player2, Match.Configuration.ScoreToWinLeg));
            }
        }

        public void Submit()
        {
            if (_currentLeg == null)
            {
                SetLeg();
            }

            StartPlayerTurn();

            Turn currentTurn = GetCurrentTurn();

            ProcessTossInputTurn(TossOneInput);
            ProcessTossInputTurn(TossTwoInput);
            ProcessTossInputTurn(TossThreeInput);

            if (currentTurn.Tosses.Any())
            {
                HandlePlayerScore();
                HandleEndTurn();
            }
        }

        public void ClearTossInputs()
        {
            TossOneInput = string.Empty;
            TossTwoInput = string.Empty;
            TossThreeInput = string.Empty;
        }

        public void Submit(KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Submit();
            }
        }
    }
}
