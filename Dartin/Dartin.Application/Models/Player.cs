using Dartin.Abstracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dartin.Models
{
    public class Player : APropertyChanged
    {
        private string _firstName;
        private string _lastName;
        public Guid Id { get; }

        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                NotifyPropertyChanged();
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                NotifyPropertyChanged();
            }
        }

        public string Name => FirstName + " " + LastName;

        public Player()
        {
            Id = Guid.NewGuid();
        }

        [JsonConstructor]
        public Player(Guid id, string firstName, string lastName)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
