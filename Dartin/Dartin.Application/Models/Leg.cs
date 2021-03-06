using System;
using Dartin.Abstracts;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Dartin.Models
{
    public class Leg : AHasWinner
    {
        private BindingList<Turn> _turns;
        private Guid _startingPlayerId;

        public int GetTotalScoreForPlayer(Player player) => _turns.Where(turn => turn.PlayerId == player.Id && turn.Valid).Sum(turn => turn.Score);
        public List<int> GetRemaindersForPlayer(Player player, int maxScore, bool onlyValid = false)
        {
            List<int> turnScores = new List<int>();
            if (!Turns.Any())
                return turnScores;

            var turns = (onlyValid) ? Turns.Where(turn => turn.PlayerId == player.Id && turn.Valid).ToList() : Turns.Where(turn => turn.PlayerId == player.Id).ToList();
            int lastValidRemainder = maxScore;
            for (int i = 0; i < turns.Count; i++)
            {
                var remainder = lastValidRemainder - turns[i].Score;
                if (turns[i].Valid)
                    lastValidRemainder = remainder;

                turnScores.Add(lastValidRemainder);
            }
            return turnScores;
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

        public Guid StartingPlayerId
        {
            get => _startingPlayerId;
            set
            {
                _startingPlayerId = value;
                NotifyPropertyChanged();
            }
        }
    }
}
