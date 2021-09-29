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

            var thimo = new Player("Thimo");
            state.Players.Add(thimo);
        }

        public void Exit()
        {
            Application.Current.Shutdown();
        }
    }
}
