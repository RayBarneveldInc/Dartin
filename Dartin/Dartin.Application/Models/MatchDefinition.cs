﻿using Dartin.Abstracts;
using Dartin.Properties;
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
                return string.Format(CultureInfo.CurrentCulture, Resources.BestOfSetsFormat, SetsToWin);
            }
            else
            {
                return string.Format(CultureInfo.CurrentCulture, Resources.BestOfLegsFormat, LegsToWinSet);
            }
        }
        private string GetMatchName()
        {
            if (Players.Count > 0)
            {
                return string.Format(CultureInfo.CurrentCulture,
                    Resources.MatchNameFormat, this.Players[0].Name, this.Players[1].Name);
            }
            else
            {
                return Resources.MatchNameDefaultText;
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
            //Math.Ceiling((decimal)Match.SetsAmount / 2) Best of?

            if (SetsToWin == GetAmountOfSetsWon(player))
            {
                WinnerId = player.Id;
                return true;
            }

            return false;
        }
    }
}
