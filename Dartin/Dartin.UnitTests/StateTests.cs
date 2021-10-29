using Dartin;
using Dartin.Models;
using Newtonsoft.Json;
using System.Linq;
using Xunit;

namespace UnitTests
{
    public class StateTests
    {
        [Fact]
        public void CheckState()
        {
            var state = State.Instance;
            State.Instance.Players.Clear();
            state.Players.Add(new Player()
            {
                FirstName = "Thimo", 
                LastName = "de Zwart"
            });
            Assert.Single(state.Players.Where(player => player.Name == "Thimo de Zwart"));
        }

        [Fact]
        public void MergeStateJoinsPlayerLists()
        {
            var stateA = State.Instance;
            stateA.Clear();
            var p = new Player("Player", "One", "NL");
            stateA.Players.Add(p);

            // Serialize stateA to get a duplicate (stateB)
            var serialized = JsonConvert.SerializeObject(stateA, Formatting.Indented);
            var stateB = JsonConvert.DeserializeObject<State>(serialized);

            stateA.Clear();
            stateA.Players.Add(new Player("Player", "Two", "NL"));

            stateA.Merge(stateB);

            Assert.Equal(2, stateA.Players.Count);
        }

        [Fact]
        public void MergeStateJoinsMatchLists()
        {
            var stateA = State.Instance;
            stateA.Clear();
            var m = new MatchDefinition();
            stateA.Matches.Add(m);

            // Serialize stateA to get a duplicate (stateB)
            var serialized = JsonConvert.SerializeObject(stateA, Formatting.Indented);
            var stateB = JsonConvert.DeserializeObject<State>(serialized);

            stateA.Clear();
            stateA.Matches.Add(new MatchDefinition());

            stateA.Merge(stateB);

            Assert.Equal(2, stateA.Matches.Count);
        }

        [Fact]
        public void MergeStateDeduplicatesPlayers()
        {
            var stateA = State.Instance;
            stateA.Clear();
            var p = new Player
            {
                FirstName = "Good",
                LastName = "Name"
            };
            stateA.Players.Add(p);

            // Serialize stateA to get a duplicate (stateB)
            var serialized = JsonConvert.SerializeObject(stateA, Formatting.Indented);
            var stateB = JsonConvert.DeserializeObject<State>(serialized);

            stateA.Clear();
            stateA.Players.Add(p);

            stateA.Merge(stateB);


            Assert.Single(stateA.Players);
        }

        [Fact]
        public void MergeStateDeduplicatesMatches()
        {
            var stateA = State.Instance;
            stateA.Clear();
            var m = new MatchDefinition();
            stateA.Matches.Add(m);

            // Serialize stateA to get a duplicate (stateB)
            var serialized = JsonConvert.SerializeObject(stateA, Formatting.Indented);
            var stateB = JsonConvert.DeserializeObject<State>(serialized);

            stateA.Clear();
            stateA.Matches.Add(m);

            stateA.Merge(stateB);


            Assert.Single(stateA.Matches);
        }

        [Fact]
        public void ClearWorksProperly()
        {
            var state = State.Instance;
            state.Clear();
            var p = new Player();
            state.Players.Add(p);
            state.Clear();
            Assert.Empty(state.Players);
        }
    }
}
