using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Dartin.Models
{
    public class Throw : INotifyPropertyChanged
    {
        private int _score;
        private int _multiplier;

        public event PropertyChangedEventHandler PropertyChanged;

        [JsonProperty("score")]
        public int Score
        {
            get => _score;
            set
            {
                if (value >= 0 && value <= 20 || value == 25 || value == 50)
                {
                    _score = value;
                    NotifyPropertyChanged();
                }
                else throw new Exception("Cannot set a score below 0 or above 20 that is not equal to 25 (bull) or 50 (bullseye).");
            }
        }

        [JsonProperty("multiplier")]
        public int Multiplier
        {
            get => _multiplier;
            set
            {
                if (value >= 1 && value <= 3)
                {
                    _multiplier = value;
                    NotifyPropertyChanged();
                }
                else throw new Exception("Cannot set a multiplier below 1 or above 3.");

            }
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        [JsonConstructor]
        public Throw(int score, int multiplier)
        {
            // Check if the score can be set on a standard dartboard
            if ((score >= 0 && score <= 20 && multiplier >= 1 && multiplier <= 3) || (score == 25 && multiplier == 1) || (score == 50 && multiplier == 1))
            {
                Score = score;
                Multiplier = multiplier;
            }
            else
                throw new Exception("Cannot create throw - invalid score and/or multiplier.");
        }

        public string GetNotation()
        {
            StringBuilder notationBuilder = new StringBuilder();

            if (Multiplier == 2)
                notationBuilder.Append('D');
            else if (Multiplier == 3)
                notationBuilder.Append('T');

            notationBuilder.Append(Score);
            return notationBuilder.ToString();
        }
    }
}
