using Core;
using Core.Models;
using Core.Repositories;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using UI.RunnerManagement.ViewModels;
using Xunit;

namespace UI.RunnerManagement.Tests.ViewModels
{
    public class RunnersViewModelTests
    {
        [Fact]
        public void Constructor_all_parameters_null_ArgumentNullException() => Assert.Throws<ArgumentNullException>(() => new RunnersViewModel(null));
        [Fact]
        public void Constructor_unitOfWork_null_ArgumentNullException() => Assert.Throws<ArgumentNullException>(() => new RunnersViewModel(null));
        [Fact]
        public void CanCreateInstance()
        {
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var vm = new RunnersViewModel(unitOfWork);
            Assert.NotNull(vm);
            Assert.NotNull(vm.EditCommand);
            Assert.NotNull(vm.InitializeCommand);
            Assert.NotNull(vm.SaveCommand);
        }
        [Fact]
        public void LoadRunners_calles_RunnersRepository_GetAll()
        {
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var runnerRepository = Substitute.For<IRunnerRepository>();
            unitOfWork.Runners.Returns(runnerRepository);

            var vm = new RunnersViewModel(unitOfWork);

            vm.LoadRunners();

            runnerRepository.Received().GetAll();
        }
        [Fact]
        public void LoadRunners_calles_RunnersRepository_GetAll2()
        {
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var runnerRepository = Substitute.For<IRunnerRepository>();
            unitOfWork.Runners.Returns(runnerRepository);
            runnerRepository.GetAll().Returns(new List<Runner>
            {
                new Runner { Id = 1 },
                new Runner { Id = 2 },
                new Runner { Id = 3 },
            });

            var vm = new RunnersViewModel(unitOfWork);

            vm.LoadRunners();

            Assert.NotNull(vm.Runners);
            Assert.NotEmpty(vm.Runners);
            Assert.Equal(3, vm.Runners.Count());
            Assert.Equal(1, vm.Runners.First().Id);
        }

        [Fact]
        public void InitializeCommand_Execute_calles_RunnersRepository_GetAll()
        {
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var runnerRepository = Substitute.For<IRunnerRepository>();
            unitOfWork.Runners.Returns(runnerRepository);

            var vm = new RunnersViewModel(unitOfWork);

            vm.InitializeCommand.Execute(null);

            runnerRepository.Received().GetAll();
        }
        [Fact]
        public void InitializeCommand_Execute_calles_RunnersRepository_GetAll2()
        {
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var runnerRepository = Substitute.For<IRunnerRepository>();
            unitOfWork.Runners.Returns(runnerRepository);
            runnerRepository.GetAll().Returns(new List<Runner>
            {
                new Runner { Id = 1 },
                new Runner { Id = 2 },
                new Runner { Id = 3 },
            });

            var vm = new RunnersViewModel(unitOfWork);

            vm.InitializeCommand.Execute(null);

            Assert.NotNull(vm.Runners);
            Assert.NotEmpty(vm.Runners);
            Assert.Equal(3, vm.Runners.Count());
            Assert.Equal(1, vm.Runners.First().Id);
        }
        [Fact]
        public void Runners_setter_raises_propertyChanged_Event()
        {
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var vm = new RunnersViewModel(unitOfWork);

            var eventWasRaised = false;
            vm.PropertyChanged += (s, a) =>
            {
                if (a.PropertyName == "Runners")
                    eventWasRaised = true;
            };

            vm.Runners = new List<Runner>();

            Assert.True(eventWasRaised);
        }
        [Fact]
        public void Runners_setter_sets_collection()
        {
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var vm = new RunnersViewModel(unitOfWork);

            var runners = new List<Runner>();

            vm.Runners = runners;

            Assert.Same(runners, vm.Runners);
        }
        [Fact]
        public void SaveRunners_calles_unitOfWork_Complete()
        {
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var vm = new RunnersViewModel(unitOfWork);

            vm.SaveRunners();

            unitOfWork.Received().Complete();
        }
        [Fact]
        public void SaveCommand_execute_calles_unitOfWork_Complete()
        {
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var vm = new RunnersViewModel(unitOfWork);

            vm.SaveCommand.Execute(null);

            unitOfWork.Received().Complete();
        }
        [Fact]
        public void EditRunner_SelectedRunner_is_new_object_calles_runnerRepository_Add()
        {
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var runnerRepository = Substitute.For<IRunnerRepository>();
            unitOfWork.Runners.Returns(runnerRepository);

            var vm = new RunnersViewModel(unitOfWork);

            var selectedRunner = new Runner { Id = 0 };
            vm.EditRunner(selectedRunner);

            runnerRepository.Received().Add(Arg.Any<Runner>());
            runnerRepository.Received().Add(selectedRunner);
        }
        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(100)]
        [InlineData(4577)]
        [InlineData(int.MaxValue)]
        public void EditRunner_SelectedRunner_is_existing_object_does_not_call_runnerRepository_Add(int runnerId)
        {
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var runnerRepository = Substitute.For<IRunnerRepository>();
            unitOfWork.Runners.Returns(runnerRepository);

            var vm = new RunnersViewModel(unitOfWork);

            var selectedRunner = new Runner { Id = runnerId };
            vm.EditRunner(selectedRunner);

            runnerRepository.DidNotReceive().Add(Arg.Any<Runner>());
        }
        [Fact]
        public void LoadCategories_calles_categoryRepository_GetAll()
        {
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var categoryRepository = Substitute.For<ICategoryRepository>();
            unitOfWork.Categories.Returns(categoryRepository);

            var vm = new RunnersViewModel(unitOfWork);

            vm.LoadCategories();

            categoryRepository.Received().GetAll();
        }
        [Fact]
        public void LoadCategories_calles_categoryRepository_GetAll2()
        {
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var categoryRepository = Substitute.For<ICategoryRepository>();
            unitOfWork.Categories.Returns(categoryRepository);
            categoryRepository.GetAll().Returns(new List<Category>
            {
                new Category { Id = 1 },
                new Category { Id = 2 },
                new Category { Id = 3 },
            });

            var vm = new RunnersViewModel(unitOfWork);

            vm.LoadCategories();

            Assert.NotNull(vm.Categories);
            Assert.NotEmpty(vm.Categories);
            Assert.Equal(3, vm.Categories.Count());
            Assert.Equal(1, vm.Categories.First().Id);
        }
        [Fact]
        public void InitializeCommand_Execute_calles_categoryRepository_GetAll()
        {
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var categoryRepository = Substitute.For<ICategoryRepository>();
            unitOfWork.Categories.Returns(categoryRepository);

            var vm = new RunnersViewModel(unitOfWork);

            vm.InitializeCommand.Execute(null);

            categoryRepository.Received().GetAll();
        }
        [Fact]
        public void InitializeCommand_Execute_calles_categoryRepository_GetAll2()
        {
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var categoryRepository = Substitute.For<ICategoryRepository>();
            unitOfWork.Categories.Returns(categoryRepository);
            categoryRepository.GetAll().Returns(new List<Category>
            {
                new Category { Id = 1 },
                new Category { Id = 2 },
                new Category { Id = 3 },
            });

            var vm = new RunnersViewModel(unitOfWork);

            vm.InitializeCommand.Execute(null);

            Assert.NotNull(vm.Categories);
            Assert.NotEmpty(vm.Categories);
            Assert.Equal(3, vm.Categories.Count());
            Assert.Equal(1, vm.Categories.First().Id);
        }
    }
}
