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
        private Player _winner = null;
        public int LegsToWinSet { get; set; }
        public int Index { get; set; }
        public List<Leg> Legs { get; set; }
        public string SetToString => String.Format("Set {0} - legs played: {1}", Index + 1, Legs.Count);
        public Player Winner
       
        {
            get => _winner;
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
