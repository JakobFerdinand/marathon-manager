using UI.RunnerManagement.ViewModels;
using Xunit;

namespace UI.RunnerManagement.Tests.ViewModels
{
    public class MainWindowViewModelTests
    {
        [Fact]
        public void CanCreateInstance()
        {
            var vm = new MainWindowViewModel();
            Assert.NotNull(vm);
        }
    }
}
