using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Dartin.Models
{
    public class Leg : INotifyPropertyChanged
    {
        private BindingList<Turn> _turns;

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        [JsonProperty("turns")]
        public BindingList<Turn> Turns
        {
            get => _turns;
            set
            {
                _turns = value;
                NotifyPropertyChanged();
            }
        }

        [JsonConstructor]
        public Leg(IList<Turn> turns)
        {
            Turns = new BindingList<Turn>(turns);
        }
    }
}
