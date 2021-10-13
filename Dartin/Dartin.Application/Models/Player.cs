using System;
using System.Collections.Generic;
using System.Text;
using Dartin.Abstracts;

namespace Dartin.Models
{
    public class Player : APropertyChanged
    {
        private string _name;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                NotifyPropertyChanged();
            }
        }
    }
}
