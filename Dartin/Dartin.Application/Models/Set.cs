using Dartin.Abstracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Dartin.Models
{
    public class Set : APropertyChanged
    {
        private BindingList<Leg> _legs;

        public BindingList<Leg> Legs
        {
            get => _legs;
            set
            {
                _legs = value;
                NotifyPropertyChanged();
            }
        }

        public Set(IList<Leg> legs)
        {
            Legs = new BindingList<Leg>(legs);
        }
    }
}
