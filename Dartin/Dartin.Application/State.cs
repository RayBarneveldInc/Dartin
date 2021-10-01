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
        private static object Locker = new object();
        private BindingList<MatchDefinition> _matches;
        private BindingList<Player> _players;

        private static readonly Lazy<State> _lazy = new Lazy<State>(() => new State());
        public static State Instance => _lazy.Value;
        public BindingList<MatchDefinition> Matches
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

        private State()
        {
            if (!Directory.Exists(Constants.SavePath))
                Directory.CreateDirectory(Constants.SavePath);

            Matches = new BindingList<MatchDefinition>();
            Players = new BindingList<Player>();

            Matches.ListChanged += Save;
            Players.ListChanged += Save;
        }

        public void Save(object sender, EventArgs e)
        {
            lock (Locker)
            {
                using (FileStream file = new FileStream(Constants.SaveFilePath, FileMode.Append, FileAccess.Write, FileShare.Read))
                using (StreamWriter writer = new StreamWriter(file, Encoding.Unicode))
                {
                    writer.Write(JsonConvert.SerializeObject(this, Formatting.Indented));
                }
            }
        }
    }
}
