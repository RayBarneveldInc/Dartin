using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Dartin.Models
{
    public class Match : INotifyPropertyChanged
    {
        private string _name;
        private BindingList<Player> _players;
        private BindingList<Set> _sets;

        [JsonProperty("players")]
        public BindingList<Player> Players
        {
            get => _players;
            set
            {
                _players = value;
                NotifyPropertyChanged();
            }
        }

        [JsonProperty("sets")]
        public BindingList<Set> Sets
        {
            get => _sets;
            set
            {
                _sets = value;
                NotifyPropertyChanged();
            }
        }

        [JsonProperty("name")]
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                NotifyPropertyChanged();
            }
        }

        [JsonProperty("date")]
        public DateTime Date { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        [JsonConstructor]
        public Match(IList<Player> players, IList<Set> sets, string name)
        {
            Players = new BindingList<Player>(players);
            Sets = new BindingList<Set>(sets);
            Name = name;
            Date = DateTime.Now.Date;
        }
    }
}
