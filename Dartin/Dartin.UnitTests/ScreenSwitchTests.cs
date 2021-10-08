using Dartin.Managers;
using Dartin.Models;
using Dartin.ViewModels;
using Xunit;

namespace UnitTests
{
    public class ScreenSwitchTests
    {
        [Fact]
        public void TestScreenSwitch()
        {
            var shell = new ShellViewModel();

            var newView = new MatchDefinitionViewModel(new MatchDefinition());
            
            ScreenManager.GetInstance().SwitchViewModel(newView);
            
            Assert.Equal(shell.ActiveItem, newView);
        }
    }
}