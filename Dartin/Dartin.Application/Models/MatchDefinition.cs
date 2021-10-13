using Dartin.Abstracts;
using Dartin.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace Dartin.Models
{
    public class MatchDefinition : AHasWinner
    {
        private string _name;
        private DateTime _date;
        private MatchConfiguration _configuration;
        private BindingList<Player> _players;
        private BindingList<Set> _sets;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                NotifyPropertyChanged();
            }
        }
        public DateTime Date
        {
            get => _date;
            set
            {
                _date = value;
                NotifyPropertyChanged();
            }
        }
        public BindingList<Player> Players
        {
            get => _players;
            set
            {
                _players = value;
                NotifyPropertyChanged();
            }
        }
        public BindingList<Set> Sets
        {
            get => _sets;
            set
            {
                _sets = value;
                NotifyPropertyChanged();
            }
        }
        public MatchConfiguration Configuration
        {
            get => _configuration;
            set
            {
                _configuration = value;
                NotifyPropertyChanged();
            }
        }

        public MatchDefinition(string name, DateTime date, BindingList<Player> players, BindingList<Set> sets, MatchConfiguration configuration)
        {
            Name = name;
            Date = date;
            Players = players;
            Sets = sets;
            Configuration = configuration;
        }
    }
}
