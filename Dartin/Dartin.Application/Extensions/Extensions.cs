using Dartin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dartin.Extensions
{
    public static class Extensions
    {
        public static Player ToPlayer(this Guid guid) => State.Instance.Players.FirstOrDefault(player => player.Id == guid);
    }
}
