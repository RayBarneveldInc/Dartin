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
        private bool _valid = true;
        private bool _winningTurn = false;
        private Guid _playerId;
        private BindingList<Toss> _tosses;

        public int Score => Tosses.Sum(toss => toss.TotalScore);

        public bool Valid {
            get => _valid;
            set
            {
                _valid = value; NotifyPropertyChanged();
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
            PlayerId = player.Id;
            Tosses = tosses;
        }
    public class Turn
    {
        public string TurnToString => String.Format("Turn {0} - total score: {1}", Index + 1, TurnScore);
        public int Index{ get; set; }
        public int TurnScore { get; set; }
        public Player Player { get; set; }
        public List<Toss> Tosses { get; set; }

        public Turn(Guid playerId, BindingList<Toss> tosses)
        {
            PlayerId = playerId;
            Tosses = tosses;
        }

    }
}
