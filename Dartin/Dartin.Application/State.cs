using Dartin.Abstracts;
using Dartin.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Dartin
{
    public class State : APropertyChanged
    {
        private BindingList<Match> _matches;
        private BindingList<Player> _players;

        public BindingList<Match> Matches
        {
            get => _matches;
            private set
            {
                _matches = value;
                NotifyPropertyChanged();
            }
        }

        public BindingList<Player> Players
        {
            get => _players;
            private set
            {
                _players = value;
                NotifyPropertyChanged();
            }
        }

        private void Save(object sender, EventArgs e) => File.WriteAllText(Path.Combine(Constants.SavePath, Constants.SaveFileName), JsonConvert.SerializeObject(this, Formatting.Indented));

        [JsonConstructor]
        public State()
        {
            if (!Directory.Exists(Constants.SavePath))
                Directory.CreateDirectory(Constants.SavePath);

            Matches = new BindingList<Match>();
            Players = new BindingList<Player>();

            Matches.ListChanged += Save;
            Players.ListChanged += Save;
        }
    }
}
