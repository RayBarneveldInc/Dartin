using Dartin.Abstracts;
using Newtonsoft.Json;
using System;
using System.Globalization;

namespace Dartin.Models
{
    public class Player : APropertyChanged
    {
        private string _firstName;
        private string _lastName;
        private string _nationality;
        public Guid Id { get; }

        public override bool Equals(object obj)
        {
            return obj != null && Equals(obj as Player);
        }

        public bool Equals(Player obj)
        {
            return obj != null && obj.Id == Id;
        }

        public override int GetHashCode() => (Id).GetHashCode();
        public string Nationality
        {
            get => _nationality;
            set
            {
                _nationality = value;
                NotifyPropertyChanged();
            }
        }
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

        [JsonIgnore]
        public string Name => FirstName + " " + LastName;

        public Player()
        {
            Id = Guid.NewGuid();
        }

        public Player(string firstName, string lastName, string nationality)
        {
            Id = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            Nationality = nationality;
        }

        [JsonConstructor]
        public Player(Guid id, string firstName, string lastName, string nationality)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Nationality = nationality;
        }
    }
}
