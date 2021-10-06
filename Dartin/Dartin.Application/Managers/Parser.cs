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
        public static bool TryParseThrow(string input, out Toss toss)
        {
            if (input.ToLower() == "bull")
            {
                toss = new Toss(50, 1);
            }
            else if (input.ToLower() == "obull")
            {
                toss = new Toss(25, 1);
            }
            else
            {
                var score = Regex.Match(input, @"^(d|t|D|T)?(\d*)$", RegexOptions.IgnoreCase);
                if (!score.Success)
                {
                    toss = null;
                    return false;
                }

                var multiplier = score.Groups[1].ToString().ToLower();
                var points = Convert.ToInt32(score.Groups[2].ToString());

                if (points > 20 && !(points == 25 || points == 50))
                {
                    toss = null;
                    return false;
                }

                if (multiplier == "d")
                {
                    toss = new Toss(points, 2);
                }

                else if (multiplier == "t")
                {
                    toss = new Toss(points, 3);
                }
                else
                {
                    toss = new Toss(points, 1);
                }
            }
            return true;
        }
    }
}
