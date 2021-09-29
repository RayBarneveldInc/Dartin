using Caliburn.Micro;
using MVVM.Models;
using Newtonsoft.Json;
using System;
using System.Windows;
using System.Net;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace MVVM.ViewModels
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
    }
}
