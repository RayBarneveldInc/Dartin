using System;
using System.Collections.Generic;
using System.Text;

namespace MVVM.Models
{
    public class Leg
    {
        // 501 of 301
        public int TurnScoreToWin { get; set; }
        public List<Turn> Turns { get; set; }

        public Leg()
        {
            Turns = new List<Turn>();
        }
    }
}
