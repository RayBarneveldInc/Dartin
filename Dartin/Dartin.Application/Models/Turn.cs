using Dartin.Abstracts;
using Dartin.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Dartin.Models
{
    public class Turn : APropertyChanged
    {
        private Guid _playerId;
        private BindingList<Toss> _tosses;

        public int TurnScore => Tosses.Sum(toss => toss.Score * toss.Multiplier);
        public Guid PlayerId
        {
            get => _playerId;
            set
            {
                _playerId = value;
                NotifyPropertyChanged();
            }
        }
        public Player Player => _playerId.ToPlayer();
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
            _playerId = player.Id;
            Tosses = tosses;
        }
        public Turn(Guid playerId, BindingList<Toss> tosses)
        {
            PlayerId = playerId;
            Tosses = tosses;
        }
    }
}
