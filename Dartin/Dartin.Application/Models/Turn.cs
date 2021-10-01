using System;
using System.Collections.Generic;
using System.Text;

namespace Dartin.Models
{
    public class Turn
    {
        public int TurnScore { get; set; }
        public Player Player { get; set; }
        public List<Throw> Throws { get; set; }

        public Turn()
        {
            Throws = new List<Throw>(3);
        }
    }
}
