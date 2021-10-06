using Dartin.Abstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Dartin.Models
{
    public class Set : AHasWinner
    {
        private BindingList<Leg> _legs;
        public BindingList<Leg> Legs {
            get => _legs;
            set
            {
                _legs = value;
                NotifyPropertyChanged();
            }
        }
        public Set(BindingList<Leg> legs)
        {
            Legs = legs;
        }
    }
}
