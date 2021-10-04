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
        public static int ParseThrow(string _throw)
        {
            if (_throw.ToLower() == "bull")
            {
                return 50;
            }
            else if (_throw.ToLower() == "obull")
            {
                return 25;
            }
            else
            {
                var score = Regex.Match(_throw, @"^(d|t|D|T)?(\d*)$", RegexOptions.IgnoreCase);
                if (!score.Success)
                {
                    return -1;
                }

                var multiplier = score.Groups[1].ToString().ToLower();
                var points = Convert.ToInt32(score.Groups[2].ToString());

                if (points > 20)
                {
                    return -1;
                }

                if (multiplier == "d") 
                {
                    return points * 2; 
                }

                else if (multiplier == "t") 
                {
                    return points * 3; 
                }

                return points;
            }
        }
    }
}
