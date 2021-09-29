using Dartin.Abstracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Dartin.Models
{
    public class Turn : APropertyChanged, INotifyPropertyChanged
    {
        private Player _player;
        private Throw _throw1;
        private Throw _throw2;
        private Throw _throw3;

        public Player Player
        {
            get => _player;
            set
            {
                _player = value;
                NotifyPropertyChanged();
            }
        }

        public Throw Throw1
        {
            get => _throw1;
            set
            {
                _throw1 = value;
                NotifyPropertyChanged();
            }
        }

        public Throw Throw2
        {
            get => _throw2;
            set
            {
                _throw2 = value;
                NotifyPropertyChanged();
            }
        }

        public Throw Throw3
        {
            get => _throw3;
            set
            {
                _throw3 = value;
                NotifyPropertyChanged();
            }
        }

        [JsonConstructor]
        public Turn(Player player, Throw throw1, Throw throw2, Throw throw3)
        {
            Player = player;
            Throw1 = throw1;
            Throw2 = throw2;
            Throw3 = throw3;
        }

        public Turn(Player player, IList<Throw> throws)
        {
            if (throws.Count != 3)
                throw new Exception("Cannot create turn because the number of throws was not equal to 3.");

            Player = player;
            Throw1 = throws[0];
            Throw2 = throws[1];
            Throw3 = throws[2];
        }
    }
}
