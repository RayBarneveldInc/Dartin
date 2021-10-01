using System;
using System.Collections.Generic;
using System.Text;

namespace MVVM.Models
{
    public class Set
    {
        public int LegsToWinSet { get; set; }
        public List<Leg> Legs { get; set; }

        public Set()
        {
            if (!LegsToWinSet.Equals(0))
            {
                Legs = new List<Leg>(LegsToWinSet);
            }
        }
    }
}
