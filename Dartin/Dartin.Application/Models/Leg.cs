using System;
using System.Collections.Generic;
using System.Text;

namespace Dartin.Models
{
    public class Leg
    {
        // 501 of 301
        public int Index { get; set; }
        public string LegToString => String.Format("Leg {0} - {1} turns", Index + 1, Turns.Count);
        public List<Turn> Turns { get; set; }
        public Player Winner { get; private set; } = null;

        public Leg()
        {
            Turns = new List<Turn>();
        }
    }
}
