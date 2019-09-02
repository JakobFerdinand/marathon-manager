using Core;
using Core.EventAggregation;
using Core.Models;
using Core.Repositories;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using UI.RunnerManagement.ViewModels;
using Xunit;

namespace UI.RunnerManagement.Tests.ViewModels
{
    public class CategoriesViewModelTests
    {
        [Fact]
        [Trait("Unit", "")]
        public void Constructor_all_parameters_null_ArgumentNullException() => Assert.Throws<ArgumentNullException>(() => new CategoriesViewModel(null, null));
        [Fact]
        [Trait("Unit", "")]
        public void Constructor_unitOfWork_null_ArgumentNullException() => Assert.Throws<ArgumentNullException>(() => new CategoriesViewModel(null, null));
        [Fact]
        [Trait("Unit", "")]
        public void CanCreateInstance()
        {
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var eventAggregator = Substitute.For<IEventAggregator>();
            var vm = new CategoriesViewModel(unitOfWork, eventAggregator);
            Assert.NotNull(vm);
            Assert.NotNull(vm.InitializeCommand);
        }
        [Fact]
        [Trait("Unit", "")]
        public void LoadCategories_calles_categoryRepository_GetAll()
        {
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var categoryRepository = Substitute.For<ICategoryRepository>();
            unitOfWork.Categories.Returns(categoryRepository);
            var eventAggregator = Substitute.For<IEventAggregator>();

            var vm = new CategoriesViewModel(unitOfWork, eventAggregator);

            vm.LoadCategories();

            categoryRepository.Received().GetAll(asNoTracking: true);
        }
        [Fact]
        [Trait("Unit", "")]
        public void LoadCategories_calles_categoryRepository_GetAll2()
        {
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var categoryRepository = Substitute.For<ICategoryRepository>();
            unitOfWork.Categories.Returns(categoryRepository);
            categoryRepository.GetAll(asNoTracking: true)
                .Returns(ImmutableList.Create(
                new Category { Id = 1 },
                new Category { Id = 2 },
                new Category { Id = 3 }));
            var eventAggregator = Substitute.For<IEventAggregator>();

            var vm = new CategoriesViewModel(unitOfWork, eventAggregator);

            vm.LoadCategories();

            Assert.NotNull(vm.Categories);
            Assert.NotEmpty(vm.Categories);
            Assert.Equal(3, vm.Categories.Count());
            Assert.Equal(1, vm.Categories.First().Id);
        }
        [Fact]
        [Trait("Unit", "")]
        public void InitializeCommand_Execute_calles_RunnersRepository_GetAll()
        {
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var categoryRepository = Substitute.For<ICategoryRepository>();
            unitOfWork.Categories.Returns(categoryRepository);
            var eventAggregator = Substitute.For<IEventAggregator>();

            var vm = new CategoriesViewModel(unitOfWork, eventAggregator);

            vm.InitializeCommand.Execute(null);

            categoryRepository.Received().GetAll(asNoTracking: true);
        }
        [Fact]
        [Trait("Unit", "")]
        public void InitializeCommand_Execute_calles_RunnersRepository_GetAll2()
        {
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var categoryRepository = Substitute.For<ICategoryRepository>();
            unitOfWork.Categories.Returns(categoryRepository);
            categoryRepository.GetAll(asNoTracking: true).Returns(ImmutableList.Create(
                new Category { Id = 1 },
                new Category { Id = 2 },
                new Category { Id = 3 }));
            var eventAggregator = Substitute.For<IEventAggregator>();

            var vm = new CategoriesViewModel(unitOfWork, eventAggregator);

            vm.InitializeCommand.Execute(null);

            Assert.NotNull(vm.Categories);
            Assert.NotEmpty(vm.Categories);
            Assert.Equal(3, vm.Categories.Count());
            Assert.Equal(1, vm.Categories.First().Id);
        }
        [Fact]
        [Trait("Unit", "")]
        public void Categories_setter_raises_propertyChanged_Event()
        {
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var eventAggregator = Substitute.For<IEventAggregator>();
            var vm = new CategoriesViewModel(unitOfWork, eventAggregator);

            var eventWasRaised = false;
            vm.PropertyChanged += (s, a) =>
            {
                if (a.PropertyName == "Categories")
                    eventWasRaised = true;
            };

            vm.Categories = new List<Category>();

            Assert.True(eventWasRaised);
        }
        [Fact]
        [Trait("Unit", "")]
        public void Categories_setter_sets_collection()
        {
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var eventAggregator = Substitute.For<IEventAggregator>();
            var vm = new CategoriesViewModel(unitOfWork, eventAggregator);

            var categories = new List<Category>();
            vm.Categories = categories;

            Assert.Same(categories, vm.Categories);
        }
    }
}
