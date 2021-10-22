using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dartin.Models
{
    public class MatchStatsPlayer
    {
        public int avgScore { get; set; }
        public int setsWon { get; set; }
        public int legsWon { get; set; }
        public int AvgScoreFirstNineDarts { get; set; }
        public int dartsThrown { get; set; }
        public int hundredEighty { get; set; }
        public int hundredSixtyPlus { get; set; }
        public int hundredFourtyPlus { get; set; }
        public int hundredTwentyPlus { get; set; }
        public int hundredPlus { get; set; }
        public int nineDarters { get; set; }
        public MatchStatsPlayer(MatchDefinition playerStats)
        {
            TotalWins(playerStats);
            MatchAverages(playerStats);
        }

        public void TotalWins(MatchDefinition match)
        {
            legsWon = 0;
            setsWon = 0;
            foreach (Set s in match.Sets)
            {
                if (s.WinnerId == match.Players[0].Id) setsWon++;
                foreach (Leg l in s.Legs) if (l.Winner.Id == match.Players[0].Id) legsWon++;
            }
        }
        public void MatchAverages(MatchDefinition match)
        {
            dartsThrown = 0;
            nineDarters = 0;
            int TotalScore = 0;
            int FirstNineDartsTotal = 0;
            int LegCount = 0;
            
            foreach (Set s in match.Sets) foreach (Leg l in s.Legs)
                {
                    LegCount += s.Legs.Count;
                    int legScoreForNineDarter = 0;
                    foreach (Turn t in l.Turns)
                    {
                        int turnTotal = 0;
                        foreach (Toss to in t.Tosses)
                        {
                            dartsThrown++;
                            turnTotal += to.TotalScore;
                            TotalScore += to.TotalScore;
                        }

                        if (l.Turns.IndexOf(t) <= 2)
                        {
                            FirstNineDartsTotal += turnTotal;
                        }

                        legScoreForNineDarter += turnTotal;
                        if (l.Turns.Count == 3 && legScoreForNineDarter == 501) nineDarters++;

                        TotalThreeDartValues(turnTotal);
                    }
                }
            AvgScoreFirstNineDarts = FirstNineDartsTotal / LegCount; 
            avgScore = TotalScore / dartsThrown;
        }

        public void TotalThreeDartValues(int turnTotal)
        {
            switch (turnTotal)
            {
                case 180:
                    hundredEighty++;
                    break;

                case >= 160:
                    hundredSixtyPlus++;
                    break;

                case >= 140:
                    hundredFourtyPlus++;
                    break;

                case >= 120:
                    hundredTwentyPlus++;
                    break;

                case >= 100:
                    hundredPlus++;
                    break;

                default:
                    break;
            }
        }
    }
}
