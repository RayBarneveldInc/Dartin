﻿using System;
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
        public List<int> GetRemainderForPlayer(Player player, int maxScore)
        {
            List<int> turnScores = new List<int>();
            if (!Turns.Any())
                return turnScores;

            for (int i = 0; i <= Turns.Count; i++)
            {
                if (Turns[i].PlayerId == player.Id)
                {
                    if (i == 0)
                        turnScores.Add(maxScore - _turns[i].Score);
                    else
                        turnScores.Add(turnScores[i - 1] - Turns[i].Score);
                }
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
    }
}
