using Caliburn.Micro;
using Dartin.Managers;

namespace Dartin.ViewModels
{
    class MainMenuViewModel : Screen, IViewModel
    {
        public string ViewName => throw new System.NotImplementedException();

        public void StartGame()
        {
            ScreenManager.GetInstance().SwitchViewModel(new MatchDefinitionViewModel());
        }

        public void Matches()
        {
            // TODO: wait for matchesviewmodel implementation
            // ScreenManager.GetInstance().SwitchViewModel(new MatchDefinitionViewModel());
        }

        public void Darters()
        {
            ScreenManager.GetInstance().SwitchViewModel(new PlayersViewModel());
        }

        public void Settings()
        {
            // ScreenManager.GetInstance().SwitchViewModel(new MatchDefinitionViewModel());
        }

        public void OnExit()
        {
            throw new System.NotImplementedException();
        }

        public MainMenuViewModel() {
        
        }
    }
}
