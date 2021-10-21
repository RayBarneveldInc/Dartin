using Dartin.Abstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;

namespace Dartin.Models
{
    public class Set : AHasWinner
    {
        private Player _winner = null;
        public int LegsToWinSet { get; set; }

        private BindingList<Leg> _legs;

        public BindingList<Leg> Legs
        {
            get => _legs;
            set
            {
                _legs = value;
            }
        }

        public Player Winner
        {
            get => _winner;
            set
            {
                _winner = value;
                NotifyPropertyChanged();
            }
        }
        public Set(BindingList<Leg> legs)
        {
            Legs = legs;
        }
    }
}
