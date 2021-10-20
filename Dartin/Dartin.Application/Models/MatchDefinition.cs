using Dartin.Abstracts;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace Dartin.Models
{
    public class MatchDefinition : APropertyChanged
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
        private int _setsToWin;
        public int SetsToWin
        {
            get
            {
                return _setsToWin;
            }
            set
            {
                _setsToWin = value;
                NotifyPropertyChanged();
            }
        }
        private int _legsToWinSet;
        public int LegsToWinSet
        {
            get
            {
                return _legsToWinSet;
            }
            set
            {
                _legsToWinSet = value;
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
            if (SetsToWin > 1)
            {
                return string.Format(CultureInfo.CurrentCulture, "Best of {0} sets", SetsToWin);
            }
            else
            {
                return string.Format(CultureInfo.CurrentCulture, "Best of {0} legs", LegsToWinSet);
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
        public double GetTurnAverage() => Sets.Sum(set => set.Legs.Sum(leg => leg.Turns.Average(turn => turn.Score)));
        public double GetAverageForPlayer(Player player)
        {
            if (Players.Contains(player))
            {
                return Sets.Sum(set => set.Legs.Sum(leg => leg.Turns.Where(turn => turn.PlayerId == player.Id).Average(turn => turn.Score)));
            }
            return -1;
        }
        public int GetAmountOfLegsWon(Player player) => Sets.Any() && Sets.Last().Legs.Any() ? Sets.Last().Legs.Count(leg => leg.WinnerId == player.Id) : 0;
        public int GetAmountOfSetsWon(Player player) => Sets.Any() ? Sets.Count(set => set.WinnerId == player.Id) : 0;
    }
}
