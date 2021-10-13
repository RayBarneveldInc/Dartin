using Dartin.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dartin.Models
{
    public class MatchConfiguration : APropertyChanged
    {
        private int _setsToWin;
        private int _scoreToWinLeg;
        private int _legsToWinSet;
        public int SetsToWin
        {
            get => _setsToWin;
            set
            {
                _setsToWin = value;
                NotifyPropertyChanged();
            }
        }
        public int LegsToWinSet
        {
            get => _legsToWinSet;
            set
            {
                _legsToWinSet = value;
                NotifyPropertyChanged();
            }
        }
        public int ScoreToWinLeg
        {
            get => _scoreToWinLeg;
            set
            {
                _scoreToWinLeg = value;
                NotifyPropertyChanged();
            }
        }
        public MatchConfiguration(int setsToWin, int legsToWinSet, int scoreToWinLeg)
        {
            SetsToWin = setsToWin;
            LegsToWinSet = legsToWinSet;
            ScoreToWinLeg = scoreToWinLeg;
        }
    }
}
