using Dartin.Managers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dartin.Models
{
    public class Player
    {
        public string Id { get; private set; }
        public string Name { get; set; }

        public Player()
        {
            Id = IdManager.GenerateId();
        }
    }
}
