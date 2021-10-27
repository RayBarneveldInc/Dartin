using Dartin.Abstracts;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace Dartin.Models
{
    public class MatchDefinition : AHasWinner
    {
        public string Name => GetMatchName();
        public string BestOfDescription => GetBestOfDescription();
        private DateTime _date;
        public Guid Id { get; }

        public override bool Equals(object obj) => obj != null && Equals(obj as MatchDefinition);
        public bool Equals(MatchDefinition obj) => obj != null && obj.Id == Id;
        public override int GetHashCode() => (Id).GetHashCode();

        public DateTime Date
        {
            get
            {
                return _date;
            }
            set
            {
                _date = value;
                NotifyPropertyChanged();
            }
        }
        private int _totalSets;
        public int TotalSets
        {
            get
            {
                return _totalSets;
            }
            set
            {
                _totalSets = value;
                NotifyPropertyChanged();
            }
        }
        private int _legsPerSet;
        public int LegsPerSet
        {
            get
            {
                return _legsPerSet;
            }
            set
            {
                _legsPerSet = value;
                NotifyPropertyChanged();
            }
        }
        private int _scoreToWinLeg;
        public int ScoreToWinLeg
        {
            get
            {
                return _scoreToWinLeg;
            }
            set
            {
                _scoreToWinLeg = value;
                NotifyPropertyChanged();
            }
        }

        private BindingList<Player> _players;
        public BindingList<Player> Players
        {
            get
            {
                return _players;
            }
            set
            {
                _players = value;
                NotifyPropertyChanged();
            }
        }
        private BindingList<Set> _sets;
        public BindingList<Set> Sets
        {
            get
            {
                return _sets;
            }
            set
            {
                _sets = value;
                NotifyPropertyChanged();
            }
        }

        public MatchDefinition()
        {
            Id = Guid.NewGuid();
            Date = DateTime.Now.Date;
            Players = new BindingList<Player>();
            Sets = new BindingList<Set>();
        }

        [JsonConstructor]
        public MatchDefinition(Guid id) => Id = id;
        private string GetBestOfDescription()
        {
            if (TotalSets > 1)
            {
                return string.Format(CultureInfo.CurrentCulture, "Best of {0} sets", TotalSets);
            }
            else
            {
                return string.Format(CultureInfo.CurrentCulture, "Best of {0} legs", LegsPerSet);
            }
        }
        private string GetMatchName()
        {
            if (Players.Count > 0)
            {
                return string.Format(CultureInfo.CurrentCulture,
                    "{0} vs {1}", this.Players[0].Name, this.Players[1].Name);
            }
            else
            {
                return "No players have been added";
            }
        }
        public double GetTurnAverage() => Sets.Sum(set => set.Legs.Sum(leg => leg.Turns.Where(turn => turn.Valid).Average(turn => turn.Score)));
        public double GetAverageForPlayer(Player player)
        {
            if (Players.Contains(player) && Sets.Any())
            {
                return Sets.Average(set => set.Legs.Average(leg => leg.Turns.Where(turn => turn.PlayerId == player.Id && turn.Valid).Average(turn => turn.Score)));
            }
            return -1;
        }
        public int GetAmountOfLegsWonOnCurrentSet(Player player) => Sets.Last().Legs.Count(leg => leg.WinnerId == player.Id);
        public int GetAmountOfSetsWon(Player player) => Sets.Count(set => set.WinnerId.Equals(player.Id));
        public bool CheckWinner(Player player)
        {
            if (Math.Ceiling((decimal)TotalSets / 2) == GetAmountOfSetsWon(player))
            {
                WinnerId = player.Id;
                return true;
            }

            return false;
        }
    }
}
