using Dartin.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dartin.Models
{
    public class Set : APropertyChanged
    {
        public string Id { get; private set; }

        private Player _winner = null;
        
        public int LegsToWinSet { get; set; }
        
        public List<Leg> Legs { get; set; }
        
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
            Id = IdManager.GenerateId();

            if (!LegsToWinSet.Equals(0))
            {
                Legs = new List<Leg>(LegsToWinSet);
            }
        }
    }
}
