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
        private static object _locker = new();
        private BindingList<MatchDefinition> _matches;
        private BindingList<Player> _players;

        private static readonly Lazy<State> _lazy = new(() => CreateStateOrLoadSaved());
        public static State Instance => _lazy.Value;
        public BindingList<MatchDefinition> Matches
        {
            get => _matches;
            set
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

        public void Clear()
        {
            Matches.Clear();
            Players.Clear();
        }

        public void Merge(State state)
        {
            Matches = new BindingList<MatchDefinition>(Matches.Union(state.Matches).ToList());
            Players = new BindingList<Player>(Players.Union(state.Players).ToList());
        }

        public void Save(string filepath)
        {
            lock (_locker)
            {
                File.WriteAllText(filepath, JsonConvert.SerializeObject(this, Formatting.Indented));
            }
        }

        private State()
        {
            if (!Directory.Exists(Constants.SavePath))
                Directory.CreateDirectory(Constants.SavePath);

            Matches = new BindingList<MatchDefinition>();
            Players = new BindingList<Player>();

            Matches.ListChanged += SaveDefault;
            Players.ListChanged += SaveDefault;
        }

        private static State CreateStateOrLoadSaved() => File.Exists(Constants.SaveFilePath) ? JsonConvert.DeserializeObject<State>(File.ReadAllText(Constants.SaveFilePath)) : new State();

        private void SaveDefault(object sender, EventArgs e) => Save(Constants.SaveFilePath);
    }
}
