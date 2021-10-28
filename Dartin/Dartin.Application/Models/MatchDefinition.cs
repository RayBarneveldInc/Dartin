﻿using Dartin.Abstracts;
using Dartin.Extensions;
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
        [JsonIgnore]
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
            get => _legsToWinSet;
            set
            {
                _legsToWinSet = value;
                NotifyPropertyChanged();
            }
        }
        private int _scoreToWinLeg;
        public int ScoreToWinLeg
        {
            get => _scoreToWinLeg;
            set
            {
                _scoreToWinLeg = value;
                NotifyPropertyChanged();
            }
        }

        private BindingList<Guid> _players;
        public BindingList<Guid> Players
        {
            get => _players;
            set
            {
                _players = value;
                NotifyPropertyChanged();
            }
        }
        private BindingList<Set> _sets;
        public BindingList<Set> Sets
        {
            get => _sets;
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
            Players = new BindingList<Guid>();
            Sets = new BindingList<Set>();
        }

        [JsonConstructor]
        public MatchDefinition(Guid id, BindingList<Guid> players)
        {
            Id = id;
            Players = players;
        }
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
        public string GetMatchName()
        {
            if (Players.Count > 0)
            {
                var player2 = Players[1].ToPlayer();
                return string.Format(CultureInfo.CurrentCulture,
                    Resources.MatchNameFormat, Players[0].ToPlayer().Name, Players[1].ToPlayer().Name);
            }
            else
            {
                return Resources.MatchNameDefaultText;
            }
        }
        public double GetTurnAverage() => Sets.Sum(set => set.Legs.Sum(leg => leg.Turns.Where(turn => turn.Valid).Average(turn => turn.Score)));
        public double GetAverageForPlayer(Guid playerId)
        {
            try
            {
                return Sets.Average(set => set.Legs.Average(leg => leg.Turns.Where(turn => turn.PlayerId == playerId && turn.Valid).Average(turn => turn.Score)));
            }
            catch
            {
                return 0;
            }
        }
        public int GetAmountOfLegsWonOnCurrentSet(Guid playerId) => Sets.Last().Legs.Count(leg => leg.WinnerId == playerId);
        public int GetAmountOfSetsWon(Guid playerId) => Sets.Count(set => set.WinnerId.Equals(playerId));
        public bool CheckWinner(Guid playerId)
        {
            //Math.Ceiling((decimal)Match.SetsAmount / 2) Best of?

            if (SetsToWin == GetAmountOfSetsWon(playerId))
            {
                WinnerId = playerId;
                return true;
            }

            return false;
        }

        [JsonIgnore]
        public Set CurrentSet => Sets.Any() ? Sets.Last() : null;

        [JsonIgnore]
        public Leg CurrentLeg => CurrentSet != null && CurrentSet.Legs.Any() ? CurrentSet.Legs.Last() : null;
    }
}
