using Dartin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Dartin.Managers
{
    public static class Parser
    {
        public static Toss ParseThrow(string _throw)
        {
            if (_throw.ToLower() == "bull")
            {
                return new Toss { Score = 50, Multiplier = 1 };
            }
            else if (_throw.ToLower() == "obull")
            {
                return new Toss { Score = 25, Multiplier = 1 };
            }
            else
            {
                var score = Regex.Match(_throw, @"^(d|t|D|T)?(\d*)$", RegexOptions.IgnoreCase);
                if (!score.Success)
                {
                    return null;
                }

                var multiplier = score.Groups[1].ToString().ToLower();
                var points = Convert.ToInt32(score.Groups[2].ToString());

                if (points > 20)
                {
                    return null;
                }

                if (multiplier == "d") 
                {
                    return new Toss { Score = points, Multiplier = 2 };
                }

                else if (multiplier == "t") 
                {
                    return new Toss { Score = points, Multiplier = 3 };
                }

                return new Toss { Score = points, Multiplier = 1 };
            }
        }
    }
}
