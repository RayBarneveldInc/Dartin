using Dartin.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dartin.Extensions
{
    public static class Extensions
    {
        public static bool TryResolveToPlayer(this Guid guid, out Player player)
        {
            player = State.Instance.Players.FirstOrDefault(player => player.Id == guid);
            return player != default(Player);
        }

        public static Player ToPlayer(this Guid guid) => State.Instance.Players.FirstOrDefault(player => player.Id == guid);
        public static bool TryRemoveLast<T>(this IList<T> list)
        {
            if (list != null && list.Any())
            {
                list.RemoveAt(list.Count - 1);
                return true;
            }
            return false;
        }
        public static int FindIndex<T>(this IEnumerable<T> items, Func<T, bool> predicate) {
            if (items == null) throw new ArgumentNullException("items");
            if (predicate == null) throw new ArgumentNullException("predicate");

            int retVal = 0;
            foreach (var item in items) {
                if (predicate(item)) return retVal;
                retVal++;
            }
            return -1;
        }
    }
}
