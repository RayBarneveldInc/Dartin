namespace Dartin.Models
{
    public class MatchStatsPlayer
    {
        public int AvgScore { get; set; }
        public int SetsWon { get; set; }
        public int LegsWon { get; set; }
        public int AvgScoreFirstNineDarts { get; set; }
        public int DartsThrown { get; set; }
        public int HundredEighty { get; set; }
        public int HundredSixtyPlus { get; set; }
        public int HundredFourtyPlus { get; set; }
        public int HundredTwentyPlus { get; set; }
        public int HundredPlus { get; set; }
        public int NineDarters { get; set; }
        public MatchStatsPlayer(MatchDefinition playerStats)
        {
            TotalWins(playerStats);
            SetMatchAverages(playerStats);
        }

        public void TotalWins(MatchDefinition match)
        {
            LegsWon = 0;
            SetsWon = 0;
            foreach (Set s in match.Sets)
            {
                if (s.WinnerId == match.Players[0]) SetsWon++;
                foreach (Leg l in s.Legs) if (l.WinnerId == match.Players[0]) LegsWon++;
            }
        }
        public void SetMatchAverages(MatchDefinition match)
        {
            int totalTurns = 0;
            int totalScore = 0;
            int firstNineDartsTotal = 0;
            int legCount = 0;

            foreach (Set set in match.Sets)
            {
                legCount += set.Legs.Count;
                foreach (Leg leg in set.Legs)
                {
                    foreach (Turn turn in leg.Turns)
                    {
                        totalScore += turn.Score;
                        totalTurns++;
                        DartsThrown += turn.Tosses.Count;
                        
                        if (leg.Turns.IndexOf(turn) <= 2)
                        {
                            firstNineDartsTotal += turn.Score;
                            if (firstNineDartsTotal == 501)
                                NineDarters++;
                        }

                        TotalThreeDartValues(turn.Score);
                    }
                }
                
                AvgScoreFirstNineDarts = firstNineDartsTotal / legCount;

                if (totalTurns == 0)
                    AvgScore = 0;
                else
                    AvgScore = totalScore / totalTurns;
            }
        }

        public void TotalThreeDartValues(int turnTotal)
        {
            switch (turnTotal)
            {
                case 180:
                    HundredEighty++;
                    break;

                case >= 160:
                    HundredSixtyPlus++;
                    break;

                case >= 140:
                    HundredFourtyPlus++;
                    break;

                case >= 120:
                    HundredTwentyPlus++;
                    break;

                case >= 100:
                    HundredPlus++;
                    break;

                default:
                    break;
            }
        }
    }
}
