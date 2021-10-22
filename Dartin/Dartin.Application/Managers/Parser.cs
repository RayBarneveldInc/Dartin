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
                return new Toss(50, 1);
            }
            else if (toss.ToLower() == "obull")
            {
                return new Toss(25, 1);
            }
            else
            {
                var score = Regex.Match(toss, @"^(d|t|D|T)?(\d*)$", RegexOptions.IgnoreCase);
                if (!score.Success)
                {
                    return null;
                }

                var multiplier = score.Groups[1].ToString().ToLower();
                var intCouldBeParsed = int.TryParse(score.Groups[2].ToString(), out var points);

                if (!intCouldBeParsed)
                    return null;

                if (points > 20 && !(points == 25 || points == 50))
                {
                    return null;
                }

                if (multiplier == "d")
                {
                    return new Toss(points, 2);
                }

                else if (multiplier == "t")
                {
                    return new Toss(points, 3);
                }

                return new Toss(points, 1);
            }
        }
    }
}
