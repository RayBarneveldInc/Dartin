using Dartin.Abstracts;
using Dartin.Managers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dartin.Models
{
    public class Toss : APropertyChanged
    {
        public string Id { get; private set; }
        public int Score { get; set; }
        public int Multiplier { get; set; };

        public Toss()
        {
            Id = IdManager.GenerateId();
        }
    }
}
