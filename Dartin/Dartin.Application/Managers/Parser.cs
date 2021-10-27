using Dartin.Models;
using Dartin.Properties;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Dartin.Managers
{
    public static class Parser
    {
        public static Toss ParseThrow(string toss)
        {
            toss = toss.ToLower(CultureInfo.CurrentCulture);

            if (toss == Resources.BullsEye)
            {
                return new Toss(50, 1);
            }
            else if (toss == Resources.OuterBullsEye)
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

                if (multiplier == Resources.Double)
                {
                    return new Toss(points, 2);
                }

                else if (multiplier == Resources.Triple)
                {
                    return new Toss(points, 3);
                }

                return new Toss(points, 1);
            }
        }
    }
}
