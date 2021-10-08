using System;
using Caliburn.Micro;
using Dartin.Managers;
using Dartin.Models;

namespace Dartin.ViewModels
{
    class MainMenuViewModel : Screen, IViewModel
    {
        public string ViewName => throw new NotImplementedException();

        public void Matches()
        {
            ScreenManager.GetInstance().SwitchViewModel(new MatchesViewModel());
        }

        public void Darters()
        {
            ScreenManager.GetInstance().SwitchViewModel(new PlayersViewModel());
        }

        public void Settings()
        {
            throw new NotImplementedException();
        }

        public void OnExit()
        {
            throw new NotImplementedException();
        }

        public MainMenuViewModel() {
        
        }
    }
}
