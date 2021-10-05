using System;
using System.Collections.Generic;
using System.Text;
using Dartin.Abstracts;
using Dartin.Managers;

namespace Dartin.Models
{
    public class Turn : APropertyChanged
    {
        public string Id { get; private set; }
        public int TurnScore { get; set; }
        public int PlayerId { get; set; }
        public List<Toss> Tosses { get; set; }

        public Turn()
        {
            Id = IdManager.GenerateId();
            Tosses = new List<Toss>(3);
        }
    }
}
