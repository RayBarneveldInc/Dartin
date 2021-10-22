﻿using Caliburn.Micro;
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
using Dartin.Converters;

namespace Dartin.ViewModels
{
    public class ScoreboardViewModel : Screen, IViewModel
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
        private Visibility _playerOneTurnIndicatorIsVisible = Visibility.Visible;
        private Visibility _playerTwoTurnIndicatorIsVisible = Visibility.Hidden;
        private Visibility _playerOneStartedLegIndicatorIsVisible = Visibility.Visible;
        private Visibility _playerTwoStartedLegIndicatorIsVisible = Visibility.Hidden;
        private BindableCollection<Turn> _player1Turns;
        private BindableCollection<Turn> _player2Turns;
        private BindableCollection<int> _player1Remainders;
        private BindableCollection<int> _player2Remainders;
        private long _player1Counter180;
        private long _player2Counter180;

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
        public Visibility PlayerOneStartedLegIndicatorIsVisible
        {
            get => _playerOneStartedLegIndicatorIsVisible;
            set
            {
                _playerOneStartedLegIndicatorIsVisible = value;
                NotifyOfPropertyChange(() => PlayerOneStartedLegIndicatorIsVisible);
            }
        }
        public Visibility PlayerTwoStartedLegIndicatorIsVisible
        {
            get => _playerTwoStartedLegIndicatorIsVisible;
            set
            {
                _playerTwoStartedLegIndicatorIsVisible = value;
                NotifyOfPropertyChange(() => PlayerTwoStartedLegIndicatorIsVisible);
            }
        }
        public string ViewName => nameof(ScoreboardViewModel);
        public static BrushColorConverter BrushColorConverter = new BrushColorConverter();
        private double _player1Average;
        private double _player2Average;

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
        public string BestOf => $"Best of {Match.SetsToWin} sets ({Match.LegsToWinSet} legs per set)";
        public MatchDefinition Match { get; }
        public long Player1Counter180
        {
            get => _player1Counter180;
            set
            {
                _player1Counter180 = value;
                NotifyOfPropertyChange(() => Player1Counter180);
            }
        }
        public long Player2Counter180
        {
            get => _player2Counter180;
            set
            {
                _player2Counter180 = value;
                NotifyOfPropertyChange(() => Player2Counter180);
            }
        }
        public double Player1Average
        {
            get => Math.Round(_player1Average, 2);
            set
            {
                _player1Average = value;
                NotifyOfPropertyChange(() => Player1Average);
            }
        }
        public double Player2Average
        {
            get => Math.Round(_player2Average, 2);
            set
            {
                _player2Average = value;
                NotifyOfPropertyChange(() => Player2Average);
            }
        }

        public ScoreboardViewModel(MatchDefinition match)
        {
            Match = match;
            SetSet();
            Player1Counter180 = Get180CounterForPlayer(Player1);
            Player2Counter180 = Get180CounterForPlayer(Player2);
        }

        private long Get180CounterForPlayer(Player player) => Match.Sets.Sum(set => set.Legs.Sum(leg => leg.Turns.Count(turn => turn.Score == 180 && turn.PlayerId == player.Id && turn.Valid)));

        public void OnExit()
        {
            throw new NotImplementedException();
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
            Player1LegScore = Match.GetAmountOfLegsWon(Player1);
            Player1SetScore = Match.GetAmountOfSetsWon(Player1);
            Player2LegScore = Match.GetAmountOfLegsWon(Player2);
            Player2SetScore = Match.GetAmountOfSetsWon(Player2);
        }

        public Turn StartPlayerTurn()
        {
            var legs = Match.Sets.Last().Legs;

            if (!_currentLeg.Turns.Any() || !_currentLeg.Turns.Last().Valid || _currentLeg.Turns.Last().WinningTurn || _currentLeg.Turns.Last().Tosses.Count == 3)
            {
                if (!_currentLeg.Turns.Any() && legs.Count >= 2 && legs[legs.Count - 2].StartingPlayerId != Guid.Empty)
                {
                    Guid previousLegStartingPlayerId = legs[legs.Count - 2].StartingPlayerId;
                    
                    if (previousLegStartingPlayerId == Player1.Id)
                    {
                        _currentLeg.Turns.Add(new Turn(Player2, new BindingList<Toss>()));
                    }
                    else
                    {
                        _currentLeg.Turns.Add(new Turn(Player1, new BindingList<Toss>()));
                    }
                }
                else if (!_currentLeg.Turns.Any() && legs.Count == 1 && Match.Sets.Count >= 2)
                {
                    var previousSet = Match.Sets[Match.Sets.Count - 2];
                    var previousLeg = previousSet.Legs.Last();
                    if (previousLeg.StartingPlayerId == Player1.Id)
                    {
                        _currentLeg.Turns.Add(new Turn(Player2, new BindingList<Toss>()));
                    }
                    else
                    {
                        _currentLeg.Turns.Add(new Turn(Player1, new BindingList<Toss>()));
                    }
                }
                else if (!_currentLeg.Turns.Any() && legs.Last().StartingPlayerId == Guid.Empty)
                {
                    _currentLeg.Turns.Add(new Turn(Player1, new BindingList<Toss>()));
                }
                else if (_currentLeg.Turns.Last().PlayerId == Player2.Id)
                {
                    _currentLeg.Turns.Add(new Turn(Player1, new BindingList<Toss>()));
                }
                else
                {
                    _currentLeg.Turns.Add(new Turn(Player2, new BindingList<Toss>()));
                }
            }

            return GetCurrentTurn();
        }

        public void TogglePlayerTurnIndicator()
        {
            if (PlayerOneTurnIndicatorIsVisible == Visibility.Visible)
            {
                PlayerOneTurnIndicatorIsVisible = Visibility.Hidden;
                PlayerTwoTurnIndicatorIsVisible = Visibility.Visible;
            }
            else
            {
                PlayerOneTurnIndicatorIsVisible = Visibility.Visible;
                PlayerTwoTurnIndicatorIsVisible = Visibility.Hidden;
            }
        }
        public void ToggleLegStartedIndicator()
        {
            if (PlayerOneStartedLegIndicatorIsVisible == Visibility.Visible)
            {
                PlayerOneStartedLegIndicatorIsVisible = Visibility.Hidden;
                PlayerTwoStartedLegIndicatorIsVisible = Visibility.Visible;
            }
            else
            {
                PlayerOneStartedLegIndicatorIsVisible = Visibility.Visible;
                PlayerTwoStartedLegIndicatorIsVisible = Visibility.Hidden;
            }
        }

        public void ProcessTossInputTurn(string tossInput)
        {
            if (!string.IsNullOrEmpty(tossInput) && Parser.ParseThrow(tossInput) != null)
            {
                Toss toss = Parser.ParseThrow(tossInput);
                Turn currentTurn = GetCurrentTurn();
                Guid activePlayerId = GetActivePlayerId();
                int currentPlayerScore = GetCurrentPlayerScore();

                if (currentPlayerScore + toss.TotalScore < Match.ScoreToWinLeg)
                {
                    currentTurn.Tosses.Add(toss);
                }
                else if (ComparePlayerScoreWithScoreToWinLeg(currentPlayerScore, toss) && toss.Multiplier == 2)
                {
                    currentTurn.Tosses.Add(toss);
                    currentTurn.WinningTurn = true;
                    _currentLeg.WinnerId = activePlayerId;
                }
                else if (
                    (ComparePlayerScoreWithScoreToWinLeg(currentPlayerScore, toss) && toss.Multiplier != 2) ||
                    (currentPlayerScore + toss.TotalScore) > Match.ScoreToWinLeg)
                {
                    currentTurn.Tosses.Add(toss);
                    currentTurn.Valid = false;
                }

                var wonLegs = Match.Sets.Last().Legs.Count(leg => leg.WinnerId == activePlayerId);

                if (Match.LegsToWinSet == wonLegs)
                {
                    _currentSet.WinnerId = activePlayerId;
                }
            }
        }

        public bool ComparePlayerScoreWithScoreToWinLeg(int currentPlayerScore, Toss toss)
        {
            return (currentPlayerScore + toss.TotalScore) == Match.ScoreToWinLeg;
        }

        public Turn GetCurrentTurn()
        {
            return _currentLeg.Turns.Last();
        }

        public Guid GetActivePlayerId()
        {
            return GetCurrentTurn().PlayerId;
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
            return _currentLeg.Turns.Where(turn => turn.PlayerId == GetActivePlayerId() && turn.Valid).Sum(turn => turn.Score);
        }

        public int CalculatePlayerScoreLeft()
        {
            return Match.ScoreToWinLeg - _currentLeg.Turns.Where(turn => turn.PlayerId == _currentLeg.Turns.Last().PlayerId && turn.Valid).Sum(turn => turn.Score);
        }

        public BindableCollection<Turn> GetPlayerTurnsCollection(Guid playerId)
        {
            return new BindableCollection<Turn>(Match.Sets.Last().Legs.Last().Turns.Where(turn => turn.PlayerId == playerId));
        }

        /// <summary>
        /// Revert turn to write new turn.
        /// </summary>
        public void RevertTurn(bool togglePlayerTurnIndicator = true)
        {
            if (_currentLeg != null && _currentLeg.Turns.Any())
            {
                var playerId = GetActivePlayerId();
                _currentLeg.Turns.TryRemoveLast();
                if (playerId == Player1.Id && togglePlayerTurnIndicator)
                {
                    Player1Remainders.TryRemoveLast();
                    Player1Turns.TryRemoveLast();
                }
                else if (togglePlayerTurnIndicator)
                {
                    Player2Remainders.TryRemoveLast();
                    Player2Turns.TryRemoveLast();
                    TogglePlayerTurnIndicator();
                }
            }
            else if (togglePlayerTurnIndicator)
            {
                MessageBox.Show("There was no turn to revert.");
            }
        }

        /// <summary>
        /// Handle scores for both players.
        /// </summary>
        public void HandlePlayerScore()
        {
            Guid activePlayerId = GetActivePlayerId();

            if (activePlayerId == Player1.Id)
            {
                Player1Turns = GetPlayerTurnsCollection(Player1.Id);
            }
            else
            {
                Player2Turns = GetPlayerTurnsCollection(Player2.Id);
            }
        }

        /// <summary>
        /// Check for the last turn of the leg.
        /// </summary>
        public void HandleLastTurn()
        {
            bool toggleTurnIndicator = true;
            bool toggleLegStartedIndicator = false;
            Turn currentTurn = GetCurrentTurn();
            Guid activePlayerId = GetActivePlayerId();

            if (currentTurn.WinningTurn)
            {
                toggleLegStartedIndicator = true;
                ClearScoreListViews();

                if (currentTurn.PlayerId != _currentLeg.StartingPlayerId)
                    toggleTurnIndicator = false;

                if (_currentSet.WinnerId == activePlayerId)
                {
                    SetSet();
                }

                SetLeg();
                SetScores();
            }

            if (!currentTurn.WinningTurn && currentTurn.Tosses.Count != 3)
                toggleTurnIndicator = false;

            if (activePlayerId == Player1.Id)
            {
                Player1Remainders = new BindableCollection<int>(_currentLeg.GetRemaindersForPlayer(Player1, Match.ScoreToWinLeg));
            }
            else
            {
                Player2Remainders = new BindableCollection<int>(_currentLeg.GetRemaindersForPlayer(Player2, Match.ScoreToWinLeg));
            }

            if (toggleTurnIndicator)
                TogglePlayerTurnIndicator();

            if (toggleLegStartedIndicator)
                ToggleLegStartedIndicator();
        }

        /// <summary>
        ///  Submit score.
        /// </summary>
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

            if ((currentTurn.Tosses.Count != 3 && !currentTurn.WinningTurn) || (currentTurn.Tosses.Any(toss => toss == null) && !currentTurn.WinningTurn))
            {
                RevertTurn(togglePlayerTurnIndicator: false);
                MessageBox.Show("Invalid turn!");
            }

            else if (currentTurn.Tosses.Any() && (currentTurn.Tosses.Count(toss => toss != null) == 3 || currentTurn.WinningTurn))
            {
                HandlePlayerScore();

                if (GetActivePlayerId() == Player1.Id)
                    Player1Average = Match.GetAverageForPlayer(Player1);
                else
                    Player2Average = Match.GetAverageForPlayer(Player2);

                HandleLastTurn();
                Player1Counter180 = Get180CounterForPlayer(Player1);
                Player2Counter180 = Get180CounterForPlayer(Player2);
            }
        }

        /// <summary>
        /// Clear all toss inputs to empty fields.
        /// </summary>
        public void ClearTossInputs()
        {
            TossOneInput = string.Empty;
            TossTwoInput = string.Empty;
            TossThreeInput = string.Empty;
        }

        /// <summary>
        /// Submit scores with enter key.
        /// </summary>
        /// <param name="e">KeyEventArgs</param>
        public void Submit(KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Submit();
            }
        }
    }
}