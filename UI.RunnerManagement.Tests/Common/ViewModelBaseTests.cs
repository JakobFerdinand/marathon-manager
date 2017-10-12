using UI.RunnerManagement.Common;
using Xunit;

namespace UI.RunnerManagement.Tests.Common
{
    public class ViewModelBaseTests
    {
        [Fact]  
        [Trait("Unit", "")]
        public void RaiseProperyChanged_should_raise_PropertyChanged_event()
        {
            var vm = new EmptyViewModelBaseSubClass();
            var eventWasRaised = false;
            vm.PropertyChanged += (sender, args) => eventWasRaised = true;

            vm.RaisePropertyChanged(null);

            Assert.True(eventWasRaised);
        }
        [Fact]
        [Trait("Unit", "")]
        public void RaisePropertyChanged_without_any_subscriber_doese_not_throw_any_exeption()
        {
            var vm = new EmptyViewModelBaseSubClass();
            vm.RaisePropertyChanged(null);
        }
        [Fact]
        [Trait("Unit", "")]
        public void RaisePropertyChanged_should_raise_PropertyChanged_with_given_propertyName()
        {
            var vm = new EmptyViewModelBaseSubClass();
            string propertyName = null;
            vm.PropertyChanged += (sender, args) => propertyName = args.PropertyName;

            vm.RaisePropertyChanged("My Fancy Property Name");

            Assert.Equal("My Fancy Property Name", propertyName);
        }
        [Fact]
        [Trait("Unit", "")]
        public void RaisePropertyChanged_called_in_setter_without_given_propertyName_should_raise_PropertyChanged_with_name_of_calling_Property()
        {
            var vm = new EmptyViewModelBaseSubClass();
            string propertyName = null;
            vm.PropertyChanged += (sender, args) => propertyName = args.PropertyName;

            vm.AnyProperty = "Any Random Value";

            Assert.Equal("AnyProperty", propertyName);
        }

        private class EmptyViewModelBaseSubClass : ViewModelBase
        {
            public string AnyProperty
            {
                set => base.RaisePropertyChanged();
            }

            public new void RaisePropertyChanged(string propertyName) => base.RaisePropertyChanged(propertyName);
        }
    }
}
