using Dartin.Abstracts;
using System.ComponentModel;

namespace Dartin.Models
{
    public class Set : AHasWinner
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

        public Set(BindingList<Leg> legs)
        {
            Legs = legs;
        }
    }
}
