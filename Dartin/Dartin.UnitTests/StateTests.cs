using Dartin;
using Dartin.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests
{
    public class StateTests
    {
        [Fact]
        public void CheckSave()
        {
            var state = State.Instance;
            state.Players.Add(new Player() { Name = "Thimo" });

            var savedState = JsonConvert.DeserializeObject<State>(File.ReadAllText(Constants.SaveFilePath));
            Assert.Single(savedState.Players.Where(player => player.Name == "Thimo"));
        }
    }
}
