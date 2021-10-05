using Dartin.Managers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dartin.Models
{
    public class Leg
    {
        public string Id { get; private set; }
        // 501 of 301
        public List<Turn> Turns { get; set; }
        public Player Winner { get; private set; } = null;

        public Leg()
        {
            Id = IdManager.GenerateId();
            Turns = new List<Turn>();
        }
    }
}
