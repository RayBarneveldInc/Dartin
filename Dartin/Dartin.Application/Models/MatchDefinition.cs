using Dartin.Abstracts;
using Dartin.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using Dartin.Abstracts;

namespace Dartin.Models
{
    public class MatchDefinition : APropertyChanged
    {
        public string Name => GetMatchName();
        public string BestOfDescription => GetBestOfDescription();

        private DateTime _date;
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

        public MatchDefinition(string name, DateTime date, BindingList<Player> players, BindingList<Set> sets, MatchConfiguration configuration)
        {
            Date = DateTime.Now;
            Players = new BindingList<Player>();
            Sets = new BindingList<Set>();
        }
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
                    "{0} vs {1}", this.Players[0].Name, this.Players[1].Name); // needs static resources
            }
            else
            {
                return "No players have been added";
            }
        }
    }
}
