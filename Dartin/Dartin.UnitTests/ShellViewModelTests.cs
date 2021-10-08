using System.Windows;
using Dartin.ViewModels;
using Xunit;

namespace UnitTests
{
    public class ShellViewModelTests
    {
        [Fact]
        public void ToggleHelpVisibility()
        {
            var vm = new ShellViewModel();
            
            vm.IsHelpVisible = Visibility.Hidden;
            
            vm.HelpClick();
            
            Assert.Equal(Visibility.Visible, vm.IsHelpVisible);
            
            vm.IsHelpVisible = Visibility.Visible;
            
            Assert.Equal(Visibility.Hidden, vm.IsHelpVisible);
        }
    }
}