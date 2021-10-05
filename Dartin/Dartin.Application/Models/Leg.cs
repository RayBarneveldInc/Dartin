using System;
using System.Collections.Generic;
using System.Text;

namespace Dartin.Models
{
    public class Leg
    {
        // 501 of 301
        public List<Turn> Turns { get; set; }
        public Player Winner { get; private set; } = null;

        public Leg()
        {
            Turns = new List<Turn>();
        }
    }
}
