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
            DartsThrown = 0;
            NineDarters = 0;
            int totalScore = 0;
            int firstNineDartsTotal = 0;
            int legCount = 0;
            
            foreach (Set set in match.Sets) foreach (Leg leg in set.Legs)
                {
                    legCount += set.Legs.Count;
                    int legScoreForNineDarter = 0;
                    foreach (Turn turn in leg.Turns)
                    {
                        int turnTotal = 0;
                        foreach (Toss toss in turn.Tosses)
                        {
                            DartsThrown++;
                            turnTotal += toss.TotalScore;
                            totalScore += toss.TotalScore;
                        }

                        if (leg.Turns.IndexOf(turn) <= 2)
                        {
                            firstNineDartsTotal += turnTotal;
                        }

                        legScoreForNineDarter += turnTotal;
                        if (leg.Turns.Count == 3 && legScoreForNineDarter == 501) NineDarters++;

                        TotalThreeDartValues(turnTotal);
                    }
                }
            if (legCount == 0)
                AvgScoreFirstNineDarts = 0;
            else
                AvgScoreFirstNineDarts = firstNineDartsTotal / legCount;

            if (DartsThrown == 0)
                AvgScore = 0;
            else
                AvgScore = totalScore / DartsThrown;
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
