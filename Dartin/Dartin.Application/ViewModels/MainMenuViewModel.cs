using System;
using Caliburn.Micro;
using Dartin.Managers;
using Dartin.Models;

namespace Dartin.ViewModels
{
    class MainMenuViewModel : Screen, IViewModel
    {
        public void Matches()
        {
            ScreenManager.GetInstance().SwitchViewModel(new MatchesViewModel(State.Instance.Matches));
        }

        public void Darters()
        {
            ScreenManager.GetInstance().SwitchViewModel(new PlayersViewModel());
        }

        public void Settings()
        {
            ScreenManager.GetInstance().SwitchViewModel(new ImportExportViewModel());
        }

        public void OnExit()
        {
            throw new NotImplementedException();
        }

        public MainMenuViewModel() {
        
        }
    }
}
