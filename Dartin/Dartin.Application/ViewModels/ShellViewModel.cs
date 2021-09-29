using Caliburn.Micro;
using Dartin.Models;
using Newtonsoft.Json;
using System;
using System.Windows;
using System.Net;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.IO;

namespace Dartin.ViewModels
{
    public class ShellViewModel : Screen
    {
        public ShellViewModel()
        {
            // on start

            State state;
            if (File.Exists(Path.Combine(Constants.SavePath, Constants.SaveFileName)))
                state = JsonConvert.DeserializeObject<State>(File.ReadAllText(Path.Combine(Constants.SavePath, Constants.SaveFileName)));
            else
                state = new State();

            /*
            Voorbeeld

            var thimo = new Player("Thimo");
            var jasper = new Player("Jasper");
            state.Players.Add(thimo);
            state.Players.Add(jasper);

            var throw1 = new Throw(20, 3);
            var throw2 = new Throw(20, 3);
            var throw3 = new Throw(20, 3);

            var turn1 = new Turn(thimo, new List<Throw> { throw1, throw2, throw3 });
            var turn2 = new Turn(jasper, new List<Throw> { throw1, throw2, throw3 });

            var leg = new Leg(new List<Turn>() { turn1, turn2 });
            var set = new Set(new List<Leg> { leg });
            state.Matches.Add(new Match(new List<Player>() { thimo, jasper }, new List<Set> { set }, "Best of 1"));
            state.Matches[0].Name = "Best of 1 maar dan anders!";
            */
        }

        public void Exit()
        {
            Application.Current.Shutdown();
        }
    }
}
