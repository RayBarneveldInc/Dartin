using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Dartin.Models
{
    public class Set : INotifyPropertyChanged
    {
        private BindingList<Leg> _legs;

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        [JsonProperty("legs")]
        public BindingList<Leg> Legs
        {
            get => _legs;
            set
            {
                _legs = value;
                NotifyPropertyChanged();
            }
        }

        [JsonConstructor]
        public Set(IList<Leg> legs)
        {
            Legs = new BindingList<Leg>(legs);
        }
    }
}
