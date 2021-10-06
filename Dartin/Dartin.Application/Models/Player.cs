using Dartin.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dartin.Models
{
    public class Player : APropertyChanged
    {
        private string _name;
        public Guid Id { get; }
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                NotifyPropertyChanged();
            }
        }
        public Player(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }
    }

}
