using Dartin.Managers;
using Dartin.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests
{
    public class MatchDefinitionTests
    {
        [Fact]
        public void TestMatchAverage()
        {
            MatchDefinition match = TestUtility.CreateExampleMatchDefinition();
            match.Sets.Add(new Set(new BindingList<Leg>()));
            match.Sets.First().Legs.Add(new Leg(new BindingList<Turn>()));
            match.Sets.First().Legs.First().Turns.Add(new Turn(match.Players[0], new BindingList<Toss>()
            {
                new Toss(20, 3),
                new Toss(20, 3),
                new Toss(10, 2)
            }));
            match.Sets.First().Legs.First().Turns.Add(new Turn(match.Players[1], new BindingList<Toss>()
            {
                new Toss(10, 3),
                new Toss(5, 2),
                new Toss(20, 1)
            }));
            var matchAverage = match.GetTurnAverage();
            Assert.Equal(100, matchAverage);

            var matchP1Average = match.GetAverageForPlayer(match.Players.First());
            Assert.Equal(140, matchP1Average);

            var matchP2Average = match.GetAverageForPlayer(match.Players[1]);
            Assert.Equal(60, matchP2Average);
        }
    }
}
