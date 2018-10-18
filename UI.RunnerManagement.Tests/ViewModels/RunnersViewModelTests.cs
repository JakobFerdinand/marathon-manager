using Core;
using Core.Models;
using Core.Repositories;
using NSubstitute;
using System;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using UI.RunnerManagement.Services;
using UI.RunnerManagement.ViewModels;
using Xunit;

namespace UI.RunnerManagement.Tests.ViewModels
{
    public class RunnersViewModelTests
    {
        [Fact]
        [Trait("Unit", "")]
        public void Constructor_all_parameters_null_ArgumentNullException() => Assert.Throws<ArgumentNullException>(() => new RunnersViewModel(null, null, null));
        [Fact]
        [Trait("Unit", "")]
        public void Constructor_unitOfWork_null_ArgumentNullException() => Assert.Throws<ArgumentNullException>(() => new RunnersViewModel(null, null, null));
        [Fact]
        [Trait("Unit", "")]
        public void CanCreateInstance()
        {
            var vm = new RunnersViewModel(() => Substitute.For<IUnitOfWork>(), Substitute.For<IDialogService>(), Substitute.For<INotificationService>());
            Assert.NotNull(vm);
            Assert.NotNull(vm.InitializeCommand);
            Assert.NotNull(vm.SaveCommand);
        }
        [Fact]
        [Trait("Unit", "")]
        public void Runners_setter_raises_propertyChanged_Event()
        {
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var vm = new RunnersViewModel(() => unitOfWork, Substitute.For<IDialogService>(), Substitute.For<INotificationService>());

            var eventWasRaised = false;
            vm.PropertyChanged += (s, a) =>
            {
                if (a.PropertyName == "Runners")
                    eventWasRaised = true;
            };

            vm.Runners = new ObservableCollection<Runner>();

            Assert.True(eventWasRaised);
        }
        [Fact]
        [Trait("Unit", "")]
        public void Runners_setter_sets_collection()
        {
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var vm = new RunnersViewModel(() => unitOfWork, Substitute.For<IDialogService>(), Substitute.For<INotificationService>());

            var runners = new ObservableCollection<Runner>();

            vm.Runners = runners;

            Assert.Same(runners, vm.Runners);
        }
        [Fact]
        [Trait("Unit", "")]
        public void SaveRunners_calles_unitOfWork_Complete()
        {
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var vm = new RunnersViewModel(() => unitOfWork, Substitute.For<IDialogService>(), Substitute.For<INotificationService>());

            vm.SaveRunners();

            unitOfWork.Received().Complete();
        }
        [Fact]
        [Trait("Unit", "")]
        public void SaveCommand_execute_calles_unitOfWork_Complete()
        {
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var vm = new RunnersViewModel(() => unitOfWork, Substitute.For<IDialogService>(), Substitute.For<INotificationService>());

            vm.SaveCommand.Execute(null);

            unitOfWork.Received().Complete();
        }
        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(100)]
        [InlineData(4577)]
        [InlineData(int.MaxValue)]
        [Trait("Unit", "")]
        public void EditRunner_SelectedRunner_is_existing_object_does_not_call_runnerRepository_Add(int runnerId)
        {
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var runnerRepository = Substitute.For<IRunnerRepository>();
            unitOfWork.Runners.Returns(runnerRepository);

            var vm = new RunnersViewModel(() => unitOfWork, Substitute.For<IDialogService>(), Substitute.For<INotificationService>());

            var selectedRunner = new Runner { Id = runnerId };

            runnerRepository.DidNotReceive().Add(Arg.Any<Runner>());
        }
    }
}
