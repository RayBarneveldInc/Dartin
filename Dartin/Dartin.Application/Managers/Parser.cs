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
        public static Toss ParseThrow(string toss)
        {
            if (toss.ToLower() == "bull")
            {
                return new Toss { Score = 50, Multiplier = 1 };
            }
            else if (toss.ToLower() == "obull")
            {
                return new Toss { Score = 25, Multiplier = 1 };
            }
            else
            {
                var score = Regex.Match(toss, @"^(d|t|D|T)?(\d*)$", RegexOptions.IgnoreCase);
                if (!score.Success)
                {
                    return null;
                }

                var multiplier = score.Groups[1].ToString().ToLower();
                var points = Convert.ToInt32(score.Groups[2].ToString());

                if (points > 20 && !(points == 25 || points == 50))
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
