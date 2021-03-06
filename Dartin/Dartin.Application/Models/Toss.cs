using Dartin.Abstracts;
using Newtonsoft.Json;

namespace Dartin.Models
{
    public class Toss : APropertyChanged
    {
        private int _score;
        private int _multiplier;
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

        [JsonIgnore]
        public int TotalScore => _score * _multiplier;

        public Toss(int score, int multiplier)
        {
            Score = score;
            Multiplier = multiplier;
        }
    }
}
