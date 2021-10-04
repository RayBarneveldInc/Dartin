using Caliburn.Micro;
using Dartin.Managers;

namespace Dartin.ViewModels
{
    class MainMenuViewModel : Screen, IViewModel
    {
        public string ViewName => throw new System.NotImplementedException();

        public void Matches()
        {
            ScreenManager.GetInstance().SwitchViewModel(new MatchDefinitionViewModel());
        }

        public void Darters()
        {
            ScreenManager.GetInstance().SwitchViewModel(new MatchDefinitionViewModel());
        }

        public void Settings()
        {
            ScreenManager.GetInstance().SwitchViewModel(new MatchDefinitionViewModel());
        }

        public void OnExit()
        {
            throw new System.NotImplementedException();
        }

        public MainMenuViewModel() {
        
        }
    }
}
