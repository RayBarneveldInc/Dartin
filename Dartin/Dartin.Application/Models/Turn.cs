using System;
using System.Collections.Generic;
using System.Text;

namespace Dartin.Models
{
    public class Turn
    {
        public string TurnToString => String.Format("Turn {0} - total score: {1}", Index + 1, TurnScore);
        public int Index{ get; set; }
        public int TurnScore { get; set; }
        public Player Player { get; set; }
        public List<Toss> Tosses { get; set; }

        public Turn()
        {
            Tosses = new List<Toss>(3);
        }
    }
}
