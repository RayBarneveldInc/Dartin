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

        private ShellViewModel _shellViewModel;

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

        public void SwitchViewModel(IViewModel viewModel)
        {
            _shellViewModel.ActivateItemAsync(viewModel);
        }
    }
}