using System;
using System.Collections.Generic;
using System.Text;

namespace Dartin.Models
{
    public class Toss
    {
        public int Index { get; set; }
        public int Score { get; set; }
        public int Multiplier { get; set; }
        public string TossToString => String.Format("Toss {0} - {1}x{2}", Index + 1, Multiplier, Score);
    }
}
