using Caliburn.Micro;
using System.Windows;
using Dartin.Managers;

namespace Dartin.ViewModels
{
    public class ShellViewModel : Conductor<IViewModel>
    {
        private Visibility _isHelpVisible;
        private bool _isBackVisible;

        public Visibility IsHelpVisible
        {
            get => _isHelpVisible;
            set
            {
                _isHelpVisible = value;
                NotifyOfPropertyChange(() => IsHelpVisible);
            }
        }

        public bool IsBackVisible
        {
            get => _isBackVisible;
            set
            {
                _isBackVisible = value;
                NotifyOfPropertyChange(() => IsBackVisible);
            }
        }

        public ShellViewModel()
        {
            ScreenManager.GetInstance().RegisterShellViewModel(this);
            IsHelpVisible = Visibility.Hidden;
            ScreenManager.GetInstance().SwitchViewModel(new MainMenuViewModel());
        }

        public void BackClick()
        {
            ScreenManager.GetInstance().SwitchViewModel(new MatchesViewModel(State.Instance.Matches));
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