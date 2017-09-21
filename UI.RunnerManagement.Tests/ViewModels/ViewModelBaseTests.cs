using UI.RunnerManagement.Common;
using Xunit;

namespace UI.RunnerManagement.Tests.ViewModels
{
    public class ViewModelBaseTests
    {
        [Fact]
        public void CanCreateInstance()
        {
            var vm = new EmptyViewModelBaseSubClass();
            Assert.NotNull(vm);
        }

        [Fact]
        public void RaisePropertyChanged_Raises_PropertyChanged_Event()
        {
            var vm = new EmptyViewModelBaseSubClass();
            var eventWasRaised = false;
            string givenPropertyName = null;
            object sender = null;

            vm.PropertyChanged += (s, e) =>
            {
                sender = s;
                givenPropertyName = e.PropertyName;
                eventWasRaised = true;
            };

            vm.CallRaisePropertyChanged("DerNameDesProperties");

            Assert.True(eventWasRaised);
            Assert.Same(vm, sender);
            Assert.Equal("DerNameDesProperties", givenPropertyName);
        }

        internal class EmptyViewModelBaseSubClass : ViewModelBase
        {
            internal void CallRaisePropertyChanged(string propertyName) => RaisePropertyChanged(propertyName);
        }
    }
}
