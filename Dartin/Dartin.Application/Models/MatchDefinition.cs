using Dartin.Managers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace Dartin.Models
{
    public class MatchDefinition
    {
        public string Id { get; private set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int SetsToWin { get; set; }
        public int LegsToWinSet { get; set; }
        public int ScoreToWinLeg { get; set; }
        public List<Player> Players { get; set; }
        public List<Set> Sets { get; set; }

        public MatchDefinition()
        {
            Id = IdManager.GenerateId();
            Players = new List<Player>(2);
            Sets = new List<Set>();
        }
    }
}
