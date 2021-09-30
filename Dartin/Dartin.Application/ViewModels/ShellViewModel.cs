using Caliburn.Micro;
using Dartin.Models;
using Newtonsoft.Json;
using System;
using System.Windows;
using System.Net;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Dartin.ViewModels
{
    public class ShellViewModel : Screen
    {
        public ShellViewModel()
        {
            // on start
        }

        public void Exit()
        {
            Application.Current.Shutdown();
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

    }
}
