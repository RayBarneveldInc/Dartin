using Dartin;
using Dartin.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    public static class TestUtility
    {
        public static MatchDefinition CreateExampleMatchDefinition()
        {
            State.Instance.Players.Add(new Player("Henk", "de Vries"));
            State.Instance.Players.Add(new Player("Jan", "Jansma"));
            MatchDefinition matchDefinition = new MatchDefinition();
            matchDefinition.Players = new BindingList<Player>() { State.Instance.Players.Last(), State.Instance.Players.ElementAt(State.Instance.Players.Count - 2) };
            matchDefinition.ScoreToWinLeg = 501;
            matchDefinition.TotalSets = 5;
            matchDefinition.LegsPerSet = 5;
            return matchDefinition;
        }

    }
}
