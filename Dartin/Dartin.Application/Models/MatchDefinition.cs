using System;
using System.Collections.Generic;

namespace Dartin.Models
{
    public class MatchDefinition
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int SetsToWin { get; set; }
        public int LegsToWinSet { get; set; }
        public int ScoreToWinLeg { get; set; }
        public List<Player> Players { get; set; }
        public List<Set> Sets { get; set; }

        public MatchDefinition()
        {
            Players = new List<Player>(2);
            Sets = new List<Set>();
        }
    }
}
