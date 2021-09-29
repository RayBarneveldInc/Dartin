using Caliburn.Micro;
using Dartin.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using MVVM.ViewModels;
using MVVM.Views;

namespace Dartin
{
    public class Bootstrapper : BootstrapperBase
    {
        public Bootstrapper()
        {
            Initialize();  
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<MatchDefinitionViewModel>();
        }
    }
}
