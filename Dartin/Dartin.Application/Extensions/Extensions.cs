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
        public static bool TryResolveToPlayer(this Guid guid, out Player player)
        {
            player = State.Instance.Players.FirstOrDefault(player => player.Id == guid);
            return player != default(Player);
        }
    }
}
