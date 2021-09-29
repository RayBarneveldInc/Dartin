using Dartin.Abstracts;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Dartin.Models
{
    public class Leg : APropertyChanged
    {
        private BindingList<Turn> _turns;

        public BindingList<Turn> Turns
        {
            get => _turns;
            set
            {
                _turns = value;
                NotifyPropertyChanged();
            }
        }
        public Leg(IList<Turn> turns)
        {
            Turns = new BindingList<Turn>(turns);
        }
    }
}
