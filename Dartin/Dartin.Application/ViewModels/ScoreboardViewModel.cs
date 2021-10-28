using Caliburn.Micro;
using Dartin.Models;
using Dartin.Extensions;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Parser = Dartin.Managers.Parser;
using System.Windows;
using Dartin.Converters;
using Dartin.Properties;

namespace Dartin.ViewModels
{
    public class ScoreboardViewModel : Screen, IViewModel
    {
        private int _player1LegScore;
        private int _player2LegScore;
        private int _player1SetScore;
        private int _player2SetScore;
        private string _tossOneInput;
        private string _tossTwoInput;
        private string _tossThreeInput;
        private BindableCollection<Turn> _player1Turns;
        private BindableCollection<Turn> _player2Turns;
        private BindableCollection<int> _player1Remainders;
        private BindableCollection<int> _player2Remainders;
        private long _player1Counter180;
        private long _player2Counter180;
        private bool _playerLegStartIndicator = true;
        private bool _playerTurnIndicator = true;
        private bool _inputIsDisabled = false;
        private bool _firstTextBoxIsFocused = false;

        public string TossOneInput
        {
            get => _tossOneInput;
            set
            {
                _tossOneInput = value;
                NotifyOfPropertyChange(() => TossOneInput);
            }
        }
        public string TossTwoInput
        {
            get => _tossTwoInput;
            set
            {
                _tossTwoInput = value;
                NotifyOfPropertyChange(() => TossTwoInput);
            }
        }
        public string TossThreeInput
        {
            get => _tossThreeInput;
            set
            {
                _tossThreeInput = value;
                NotifyOfPropertyChange(() => TossThreeInput);
            }
        }
        public static BrushColorConverter BrushColorConverter = new BrushColorConverter();
        private double _player1Average = 0;
        private double _player2Average = 0;

        public Player Player1 => Match.Players.First().ToPlayer();
        public Player Player2 => Match.Players[1].ToPlayer();
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
            get => _player2LegScore;
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
            get => _player2SetScore;
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

        public bool PlayerTurnIndicator
        {
            get => _playerTurnIndicator;
            set
            {
                _playerTurnIndicator = value;
                NotifyOfPropertyChange(() => PlayerTurnIndicator);
            }
        }
        public bool PlayerLegStartIndicator
        {
            get => _playerLegStartIndicator;
            set
            {
                _playerLegStartIndicator = value;
                NotifyOfPropertyChange(() => PlayerLegStartIndicator);
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

        public bool InputIsDisabled
        {
            get => _inputIsDisabled;
            set
            {
                _inputIsDisabled = value;
                NotifyOfPropertyChange(() => InputIsDisabled);
            }
        }

        public bool FirstTextBoxIsFocused
        {
            get => _firstTextBoxIsFocused;
            set
            {
                _firstTextBoxIsFocused = value;
                NotifyOfPropertyChange(() => FirstTextBoxIsFocused);
                _firstTextBoxIsFocused = false;
                NotifyOfPropertyChange(() => FirstTextBoxIsFocused);
            }
        }
        public bool MessageBoxEnabled { get; set; } = true;

        public ScoreboardViewModel(MatchDefinition match)
        {
            Match = match;
            Match.PropertyChanged += WriteToState;
            Match.Sets.ListChanged += WriteToState;

            if (!Match.Sets.Any())
            {
                AddSet();
                AddLeg();
            }
            else if (Match.CurrentLeg != null)
            {
                Player1Turns = GetPlayerTurnsCollection(Player1.Id);
                Player2Turns = GetPlayerTurnsCollection(Player2.Id);
                Player1Remainders = new BindableCollection<int>(Match.CurrentLeg.GetRemaindersForPlayer(Player1, Match.ScoreToWinLeg));
                Player2Remainders = new BindableCollection<int>(Match.CurrentLeg.GetRemaindersForPlayer(Player2, Match.ScoreToWinLeg));
                if (Match.CurrentLeg.Turns.Any())
                {
                    PlayerTurnIndicator = Match.CurrentLeg.Turns.Last().PlayerId == Player2.Id;
                }
                else
                {
                    PlayerTurnIndicator = Match.CurrentLeg.StartingPlayerId == Player2.Id;
                }
                PlayerLegStartIndicator = Match.CurrentLeg.StartingPlayerId == Player1.Id;
                Player1Average = Match.GetAverageForPlayer(Player1.Id);
                Player2Average = Match.GetAverageForPlayer(Player2.Id);
            }

            Player1Counter180 = Get180CounterForPlayer(Player1);
            Player2Counter180 = Get180CounterForPlayer(Player2);
        }

        private void WriteToState(object sender, EventArgs e)
        {
            int stateMatchIndex = State.Instance.Matches.FindIndex(match => match.Id == Match.Id);
            State.Instance.Matches[stateMatchIndex] = Match;
        }

        private long Get180CounterForPlayer(Player player) => Match.Sets.Sum(set => set.Legs.Sum(leg => leg.Turns.Count(turn => turn.Score == 180 && turn.PlayerId == player.Id && turn.Valid)));

        public void OnExit() => throw new NotImplementedException();

        public void AddLeg()
        {
            if (Match.CurrentLeg != null)
                Match.CurrentLeg.Turns.ListChanged -= WriteToState;

            Match.CurrentSet.Legs.Add(new Leg(new BindingList<Turn>()));
            Match.CurrentLeg.Turns.ListChanged += WriteToState;
        }

        public void AddSet() => Match.Sets.Add(new Set(new BindingList<Leg>()));

        public void SetScores()
        {
            Player1LegScore = Match.GetAmountOfLegsWonOnCurrentSet(Player1.Id);
            Player1SetScore = Match.GetAmountOfSetsWon(Player1.Id);
            Player2LegScore = Match.GetAmountOfLegsWonOnCurrentSet(Player2.Id);
            Player2SetScore = Match.GetAmountOfSetsWon(Player2.Id);
        }

        public Turn StartPlayerTurn()
        {
            var legs = Match.CurrentSet.Legs;

            if (Match.CurrentLeg.Turns.Any())
                Match.CurrentLeg.Turns.Last().Tosses.ListChanged -= WriteToState;

            if (!Match.CurrentLeg.Turns.Any() || !Match.CurrentLeg.Turns.Last().Valid || Match.CurrentLeg.Turns.Last().WinningTurn || Match.CurrentLeg.Turns.Last().Tosses.Count == 3)
            {
                if (!Match.CurrentLeg.Turns.Any() && legs.Count >= 2 && legs[legs.Count - 2].StartingPlayerId != Guid.Empty)
                {
                    Guid previousLegStartingPlayerId = legs[legs.Count - 2].StartingPlayerId;
                    
                    if (previousLegStartingPlayerId == Player1.Id)
                    {
                        Match.CurrentLeg.Turns.Add(new Turn(Player2, new BindingList<Toss>()));
                    }
                    else
                    {
                        Match.CurrentLeg.Turns.Add(new Turn(Player1, new BindingList<Toss>()));
                    }
                }
                else if (!Match.CurrentLeg.Turns.Any() && legs.Count == 1 && Match.Sets.Count >= 2)
                {
                    var previousSet = Match.Sets[Match.Sets.Count - 2];
                    var previousLeg = previousSet.Legs.Last();
                    if (previousLeg.StartingPlayerId == Player1.Id)
                    {
                        Match.CurrentLeg.Turns.Add(new Turn(Player2, new BindingList<Toss>()));
                    }
                    else
                    {
                        Match.CurrentLeg.Turns.Add(new Turn(Player1, new BindingList<Toss>()));
                    }
                }
                else if (!Match.CurrentLeg.Turns.Any() && Match.CurrentLeg.StartingPlayerId == Guid.Empty)
                {
                    Match.CurrentLeg.Turns.Add(new Turn(Player1, new BindingList<Toss>()));
                }
                else if (Match.CurrentLeg.Turns.Last().PlayerId == Player2.Id)
                {
                    Match.CurrentLeg.Turns.Add(new Turn(Player1, new BindingList<Toss>()));
                }
                else
                {
                    Match.CurrentLeg.Turns.Add(new Turn(Player2, new BindingList<Toss>()));
                }
            }
            Match.CurrentLeg.Turns.Last().Tosses.ListChanged += WriteToState;

            return GetCurrentTurn();
        }

        public void ProcessTossInputTurn(string tossInput)
        {
            if (!string.IsNullOrEmpty(tossInput) && Parser.ParseThrow(tossInput) != null)
            {
                Toss toss = Parser.ParseThrow(tossInput);
                Guid activePlayerId = GetActivePlayerId();
                int currentPlayerScore = GetCurrentPlayerScore();

                if (currentPlayerScore + toss.TotalScore < Match.ScoreToWinLeg)
                {
                    GetCurrentTurn().Tosses.Add(toss);
                }
                else if (ComparePlayerScoreWithScoreToWinLeg(currentPlayerScore, toss) && toss.Multiplier == 2)
                {
                    GetCurrentTurn().Tosses.Add(toss);
                    GetCurrentTurn().WinningTurn = true;
                    Match.CurrentLeg.WinnerId = activePlayerId;
                }
                else if (
                    (ComparePlayerScoreWithScoreToWinLeg(currentPlayerScore, toss) && toss.Multiplier != 2) ||
                    (currentPlayerScore + toss.TotalScore) > Match.ScoreToWinLeg)
                {
                    GetCurrentTurn().Tosses.Add(toss);
                    GetCurrentTurn().Valid = false;
                }

                var wonLegs = Match.Sets.Last().Legs.Count(leg => leg.WinnerId == activePlayerId);

                if (Match.LegsToWinSet == wonLegs)
                {
                    Match.CurrentSet.WinnerId = activePlayerId;
                }
            }
        }

        public bool ComparePlayerScoreWithScoreToWinLeg(int currentPlayerScore, Toss toss) => (currentPlayerScore + toss.TotalScore) == Match.ScoreToWinLeg;

        public Turn GetCurrentTurn() => Match.CurrentLeg.Turns.Last();

        public Guid GetActivePlayerId() => GetCurrentTurn().PlayerId;

        public void ClearScoreListViews()
        {
            Player1Turns.Clear();
            Player2Turns.Clear();
            Player1Remainders.Clear();
            Player2Remainders.Clear();
        }

        public int GetCurrentPlayerScore() => Match.CurrentLeg.Turns.Where(turn => turn.PlayerId == GetActivePlayerId() && turn.Valid).Sum(turn => turn.Score);

        public int CalculatePlayerScoreLeft() => Match.ScoreToWinLeg - Match.CurrentLeg.Turns.Where(turn => turn.PlayerId == Match.CurrentLeg.Turns.Last().PlayerId && turn.Valid).Sum(turn => turn.Score);

        public BindableCollection<Turn> GetPlayerTurnsCollection(Guid playerId) => new BindableCollection<Turn>(Match.CurrentLeg.Turns.Where(turn => turn.PlayerId == playerId));

        public void RevertTurn(bool dontTogglePlayerTurnIndicator = false)
        {
            dontTogglePlayerTurnIndicator = !dontTogglePlayerTurnIndicator;
            if (Match.CurrentLeg != null && Match.CurrentLeg.Turns.Any())
            {
                var playerId = GetActivePlayerId();
                Match.CurrentLeg.Turns.TryRemoveLast();
                if (playerId == Player1.Id && dontTogglePlayerTurnIndicator)
                {
                    Player1Remainders.TryRemoveLast();
                    Player1Turns.TryRemoveLast();
                    PlayerTurnIndicator = !PlayerTurnIndicator;
                }
                else if (dontTogglePlayerTurnIndicator)
                {
                    Player2Remainders.TryRemoveLast();
                    Player2Turns.TryRemoveLast();
                    PlayerTurnIndicator = !PlayerTurnIndicator;
                }

                if (!Match.CurrentLeg.Turns.Any())
                    Match.CurrentSet.Legs.RemoveAt(Match.CurrentSet.Legs.Count - 1);
            }
            else if (dontTogglePlayerTurnIndicator)
            {
                if (MessageBoxEnabled)
                    MessageBox.Show(Resources.NoTurnToRevertMessage, Resources.NoTurnToRevertTitle, MessageBoxButton.OK);
            }
        }

        public void HandlePlayerScore()
        {
            Guid activePlayerId = GetActivePlayerId();

            if (activePlayerId == Player1.Id)
                Player1Turns = GetPlayerTurnsCollection(Player1.Id);
            else
                Player2Turns = GetPlayerTurnsCollection(Player2.Id);
        }

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

                if (currentTurn.PlayerId != Match.CurrentLeg.StartingPlayerId)
                    toggleTurnIndicator = false;

                if (Match.CurrentSet.WinnerId == activePlayerId)
                {
                    AddSet();

                    if (Match.CheckWinner(activePlayerId))
                    {
                        var activePlayer = activePlayerId.ToPlayer();
                        InputIsDisabled = true;

                        if (MessageBoxEnabled)
                            MessageBox.Show(string.Format(Resources.MatchWonMessage, activePlayer.Name), Resources.MatchWonTitle, MessageBoxButton.OK);
                    }
                }

                AddLeg();
                SetScores();
            }

            if (!currentTurn.WinningTurn && currentTurn.Tosses.Count != 3)
                toggleTurnIndicator = false;

            if (activePlayerId == Player1.Id)
            {
                Player1Remainders = new BindableCollection<int>(Match.CurrentLeg.GetRemaindersForPlayer(Player1, Match.ScoreToWinLeg));
            }
            else
            {
                Player2Remainders = new BindableCollection<int>(Match.CurrentLeg.GetRemaindersForPlayer(Player2, Match.ScoreToWinLeg));
            }

            if (toggleTurnIndicator)
                PlayerTurnIndicator = !PlayerTurnIndicator;

            if (toggleLegStartedIndicator)
                PlayerLegStartIndicator = !PlayerLegStartIndicator; 
        }

        public void Submit()
        {
            if (Match.WinnerId != Guid.Empty)
                return;

            if (Match.CurrentLeg == null || Match.CurrentLeg.WinnerId != Guid.Empty)
            {
                AddLeg();
            }

            StartPlayerTurn();

            Turn currentTurn = GetCurrentTurn();

            ProcessTossInputTurn(TossOneInput);
            ProcessTossInputTurn(TossTwoInput);
            ProcessTossInputTurn(TossThreeInput);

            if ((currentTurn.Tosses.Count != 3 && !currentTurn.WinningTurn) || (currentTurn.Tosses.Any(toss => toss == null) && !currentTurn.WinningTurn))
            {
                RevertTurn(dontTogglePlayerTurnIndicator: true);

                if (MessageBoxEnabled)
                    MessageBox.Show(Resources.InvalidTurnMessage, Resources.InvalidTurnTitle, MessageBoxButton.OK);
            }
            else if (currentTurn.Tosses.Any() && (currentTurn.Tosses.Count(toss => toss != null) == 3 || currentTurn.WinningTurn))
            {
                HandlePlayerScore();

                if (GetActivePlayerId() == Player1.Id)
                    Player1Average = Match.GetAverageForPlayer(Player1.Id);
                else
                    Player2Average = Match.GetAverageForPlayer(Player2.Id);

                HandleLastTurn();
                Player1Counter180 = Get180CounterForPlayer(Player1);
                Player2Counter180 = Get180CounterForPlayer(Player2);
            }
            ClearTossInputs();
            FirstTextBoxIsFocused = true;
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
                Submit();
        }
    }
}