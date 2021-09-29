using Dartin.Abstracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Dartin.Models
{
    public class Match : APropertyChanged
    {
        private string _name;
        private BindingList<Player> _players;
        private BindingList<Set> _sets;

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

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                NotifyPropertyChanged();
            }
        }

        public DateTime Date { get; }

        public Match(IList<Player> players, IList<Set> sets, string name)
        {
            Players = new BindingList<Player>(players);
            Sets = new BindingList<Set>(sets);
            Name = name;
            Date = DateTime.Now.Date;
        }
    }
}
