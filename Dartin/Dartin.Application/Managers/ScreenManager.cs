using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Documents;
using Caliburn.Micro;
using Dartin.ViewModels;

namespace Dartin.Managers
{
    public class ScreenManager
    {
        private static ScreenManager _instance;
        public IViewModel Previous;
        private ShellViewModel _shellViewModel;

        private void ShowBackButtonIfScoreboardViewModel()
        {
            var currentViewModel = _shellViewModel.ActiveItem.GetType();
            if (currentViewModel == typeof(ScoreboardViewModel) || currentViewModel == typeof(MatchReportViewModel))
                _shellViewModel.IsBackVisible = true;
            else
                _shellViewModel.IsBackVisible = false;
        }

        private ScreenManager()
        {
            _instance = this;
        }

        public static ScreenManager GetInstance()
        {
            return _instance ?? new ScreenManager();
        }

        public void RegisterShellViewModel(ShellViewModel shell)
        {
            _shellViewModel ??= shell;
        }

        public void RevertToPreviousViewModel()
        {
            if (Previous == null)
            {
                return;
            }
            
            _shellViewModel.ActivateItemAsync(Previous);
        }

        public void SwitchViewModel(IViewModel viewModel)
        {
            Previous = _shellViewModel.ActiveItem;
            
            _shellViewModel.ActivateItemAsync(viewModel);

            ShowBackButtonIfScoreboardViewModel();
        }
    }
}