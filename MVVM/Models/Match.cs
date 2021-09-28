using System;
using System.Collections.Generic;
using System.Text;

namespace MVVM.Models
{
    public class Match
    {
        public List<Player> Players;
        public List<Set> Sets;
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
    }
}
