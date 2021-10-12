using System;
using Dartin.Abstracts;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Dartin.Extensions;
using System.Linq;

namespace Dartin.Models
{
    public class Leg : AHasWinner
    {
        private BindingList<Turn> _turns;
        public int GetTotalScoreForPlayer(Player player) => _turns.Where(turn => turn.PlayerId == player.Id && turn.Valid).Sum(turn => turn.Score);
        public List<int> GetRemaindersForPlayer(Player player, int maxScore)
        {
            List<int> turnScores = new List<int>();
            if (!Turns.Any())
                return turnScores;

            var turns = Turns.Where(turn => turn.PlayerId == player.Id && turn.Valid).ToList();
            for (int i = 0; i < turns.Count; i++)
            {
                if (i == 0)
                    turnScores.Add(maxScore - turns[i].Score);
                else
                    turnScores.Add(turnScores[i - 1] - turns[i].Score);
            }
            return turnScores;
        }
        public int GetRemainderForPlayer(Player player, int maxScore)
        {
            var remainders = GetRemaindersForPlayer(player, maxScore);
            if (remainders.Any())
                return remainders.Sum();
            else
                return maxScore;
        }
        public BindingList<Turn> Turns
        {
            get => _turns;
            set
            {
                _turns = value;
                NotifyPropertyChanged();
            }
        }
        public Leg(BindingList<Turn> turns)
        {
            Turns = turns;
        }
    }
}
