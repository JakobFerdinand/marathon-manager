using System;
using UI.RunnerManagement.Common;
using Xunit;

namespace UI.RunnerManagement.Tests.Common
{
    public class RelayCommandTests
    {
        [Fact]
        public void Constructor_execute_null_should_throw_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new RelayCommand(null));
            Assert.Throws<ArgumentNullException>(() => new RelayCommand(null, null));
            Assert.Throws<ArgumentNullException>(() => new RelayCommand<string>(null));
            Assert.Throws<ArgumentNullException>(() => new RelayCommand<string>(null, null));
        }
        [Fact]
        public void CanCreateInstance_without_given_canExecute()
        {
            var command = new RelayCommand(() => { });
            Assert.NotNull(command);
        }
        [Fact]
        public void CanCreateGenericInstance_without_given_canExecute()
        {
            var genericCommand = new RelayCommand<string>(_ => { });
            Assert.NotNull(genericCommand);
        }
        [Fact]
        public void CanCreateInstance_with_given_canExecute()
        {
            var command = new RelayCommand(() => { }, () => true);
            Assert.NotNull(command);
        }
        [Fact]
        public void CanCreateGenericInstance_with_given_canExecute()
        {
            var genericCommand = new RelayCommand<string>(_ => { }, _ => true);
            Assert.NotNull(genericCommand);
        }
        [Fact]
        public void Execute_should_call_given_execute_method()
        {
            var methodWasCalled = false;
            var command = new RelayCommand(() => methodWasCalled = true);

            command.Execute(null);

            Assert.True(methodWasCalled);
        }
        [Fact]
        public void Generic_Execute_should_call_given_execute_method()
        {
            var methodWasCalled = false;
            var command = new RelayCommand<string>(_ => methodWasCalled = true);

            command.Execute(null);

            Assert.True(methodWasCalled);
        }
        [Fact]
        public void Generic_Execute_should_call_given_execute_method_with_given_parameter()
        {
            string valueFromParameter = null;
            var command = new RelayCommand<string>(v => valueFromParameter = v);

            command.Execute("the Value from the parameter");

            Assert.Equal("the Value from the parameter", valueFromParameter);
        }
        [Fact]
        public void CanExecute_should_return_true_when_given_canExecute_method_is_null()
        {
            var command = new RelayCommand(() => { });

            var canExecute = command.CanExecute(null);

            Assert.True(canExecute);
        }
        [Fact]
        public void Generic_CanExecute_should_return_true_when_given_canExecute_method_is_null()
        {
            var command = new RelayCommand<string>(_ => { });

            var canExecute = command.CanExecute(null);

            Assert.True(canExecute);
        }
        [Fact]
        public void CanExecute_should_call_given_canExecute_method()
        {
            var methodWasCalled = false;
            var command = new RelayCommand(
                () => { },
                () =>
                {
                    methodWasCalled = true;
                    return true;
                });

            var canExecute = command.CanExecute(null);

            Assert.True(methodWasCalled);
            Assert.True(canExecute);
        }
        [Fact]
        public void Generic_CanExecute_should_call_given_canExecute_method()
        {
            var methodWasCalled = false;
            var command = new RelayCommand<int>(
                _ => { },
                _ =>
                {
                    methodWasCalled = true;
                    return true;
                });

            var canExecute = command.CanExecute(default(int));

            Assert.True(methodWasCalled);
            Assert.True(canExecute);
        }
        [Fact]
        public void Generic_CanExecute_should_call_given_canExecute_method_with_given_parameter()
        {
            string valueFromParameter = null;
            var command = new RelayCommand<string>(
                _ => { },
                v =>
                {
                    valueFromParameter = v;
                    return false;
                });

            var canExecute = command.CanExecute("the Value from the parameter");

            Assert.Equal("the Value from the parameter", valueFromParameter);
            Assert.False(canExecute);
        }
    }
}
