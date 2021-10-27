using Dartin.Extensions;
using Dartin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dartin.Abstracts
{
    public abstract class AHasWinner : APropertyChanged
    {
        private Guid _winnerId;
        public Guid WinnerId
        {
            get => _winnerId;
            set
            {
                _winnerId = value;
                NotifyPropertyChanged();
            }
        }
    }
}
