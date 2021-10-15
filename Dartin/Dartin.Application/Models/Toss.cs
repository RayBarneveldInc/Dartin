using Dartin.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dartin.Models
{
    public class Toss : APropertyChanged
    {
        private int _score;
        private int _multiplier;
        public int Index { get; set; }
        public string TossToString => String.Format("Toss {0} - {1}x{2}", Index + 1, Multiplier, Score);
        public int Score
        {
            get => _score;
            set
            {
                _score = value;
                NotifyPropertyChanged();
            }
        }

        public int Multiplier
        {
            get => _multiplier;
            set
            {
                _multiplier = value;
                NotifyPropertyChanged();
            }
        }

        public int TotalScore => _score * _multiplier;

        public Toss(int score, int multiplier)
        {
            Score = score;
            Multiplier = multiplier;
        }
    }
}
