using Dartin.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dartin.Models
{
    public class Set : APropertyChanged
    {
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
                NotifyPropertyChanged();
            }
        }

        public Set()
        {
            if (!LegsToWinSet.Equals(0))
            {
                Legs = new List<Leg>(LegsToWinSet);
            }
        }
    }
}
