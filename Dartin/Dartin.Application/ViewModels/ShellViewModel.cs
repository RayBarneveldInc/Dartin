﻿using Caliburn.Micro;
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

        private int _selectedViewIndex;
        public BindableCollection<Type> ViewModels { get; set; }

        public int SelectedViewIndex
        {
            get => _selectedViewIndex;
            set
            {
                _selectedViewIndex = value;
                SwitchView(_selectedViewIndex);
            }
        }

        public ShellViewModel()
        {
            ScreenManager.GetInstance().RegisterShellViewModel(this);

            IsHelpVisible = Visibility.Hidden;
            
            ViewModels = new BindableCollection<Type>();

            Assembly.GetExecutingAssembly().GetTypes()
                .Where(x => x.GetInterfaces().Contains(typeof(IViewModel)))
                .ToList().ForEach(x=> ViewModels.Add(x));
            
            NotifyOfPropertyChange(() => ViewModels);
        }

        public void HelpClick()
        {
            if (IsHelpVisible == Visibility.Visible)
            {
                IsHelpVisible = Visibility.Hidden;
            }
            else
            {
                IsHelpVisible = Visibility.Visible;
            }
        }

        private void SwitchView(int index)
        {
            if (index >= 0 && index < ViewModels.Count)
            {
                ScreenManager.GetInstance().SwitchViewModel(
                    Activator.CreateInstance(ViewModels[index]) as IViewModel);
            }
        }
    }
}