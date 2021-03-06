using Dartin.Abstracts;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Linq;

namespace Dartin.Models
{
    public class Turn : APropertyChanged
    {
        private bool _valid = true;
        private bool _winningTurn = false;
        private Guid _playerId;
        private BindingList<Toss> _tosses;

        [JsonIgnore]
        public int Score => Tosses.Sum(toss => toss.TotalScore);
        public bool Valid {
            get => _valid;
            set
            {
                _valid = value;
                NotifyPropertyChanged();
            }
        }

        public bool WinningTurn
        {
            get => _winningTurn;
            set
            {
                _winningTurn = value;
                NotifyPropertyChanged();
            }
        }

        public Guid PlayerId
        {
            get => _playerId;
            set
            {
                _playerId = value;
                NotifyPropertyChanged();
            }
        }

        public BindingList<Toss> Tosses
        {
            get => _tosses;
            set
            {
                _tosses = value;
                NotifyPropertyChanged();
            }
        }

        public Turn(Player player, BindingList<Toss> tosses)
        {
            if (player == null)
                PlayerId = Guid.Empty;
            else
                PlayerId = player.Id;

            Tosses = tosses;
        }

        [JsonConstructor]
        public Turn(Guid playerId, BindingList<Toss> tosses)
        {
            PlayerId = playerId;
            Tosses = tosses;
        }
    }
}
