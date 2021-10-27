using Caliburn.Micro;
using Dartin.Models;
using Newtonsoft.Json;
using System;
using System.Windows;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Dartin.Managers;

namespace Dartin.ViewModels
{
    public class ShellViewModel : Conductor<IViewModel>
    {
        private Visibility _isHelpVisible;

        public Visibility IsHelpVisible
        {
            get => _isHelpVisible;
            set
            {
                _isHelpVisible = value;
                NotifyOfPropertyChange(() => IsHelpVisible);
            }
        }

        public ShellViewModel()
        {
            ScreenManager.GetInstance().RegisterShellViewModel(this);
            IsHelpVisible = Visibility.Hidden;
            ScreenManager.GetInstance().SwitchViewModel(new MainMenuViewModel());
        }

        public void HelpClick()
        {
            if (IsHelpVisible == Visibility.Visible)
                IsHelpVisible = Visibility.Hidden;
            else
                IsHelpVisible = Visibility.Visible;
        }
    }
}