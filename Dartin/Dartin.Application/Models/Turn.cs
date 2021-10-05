using System;
using System.Collections.Generic;
using System.Text;

namespace Dartin.Models
{
    public class Turn
    {
        public int TurnScore { get; set; }
        public Player Player { get; set; }
        public List<Toss> Tosses { get; set; }

        public Turn()
        {
            Tosses = new List<Toss>(3);
        }
    }
}
