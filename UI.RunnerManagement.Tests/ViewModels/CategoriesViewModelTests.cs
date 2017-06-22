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
    public class CategoriesViewModelTests
    {
        [Fact]
        public void Constructor_all_parameters_null_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new CategoriesViewModel(null));
        }
        [Fact]
        public void Constructor_unitOfWork_null_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new CategoriesViewModel(null));
        }
        [Fact]
        public void CanCreateInstance()
        {
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var vm = new CategoriesViewModel(unitOfWork);
            Assert.NotNull(vm);
            Assert.NotNull(vm.InitializeCommand);
        }
        [Fact]
        public void LoadCategories_calles_categoryRepository_GetAll()
        {
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var categoryRepository = Substitute.For<ICategoryRepository>();
            unitOfWork.Categories.Returns(categoryRepository);

            var vm = new CategoriesViewModel(unitOfWork);

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

            var vm = new CategoriesViewModel(unitOfWork);

            vm.LoadCategories();

            Assert.NotNull(vm.Categories);
            Assert.NotEmpty(vm.Categories);
            Assert.Equal(3, vm.Categories.Count());
            Assert.Equal(1, vm.Categories.First().Id);
        }
        [Fact]
        public void InitializeCommand_Execute_calles_RunnersRepository_GetAll()
        {
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var categoryRepository = Substitute.For<ICategoryRepository>();
            unitOfWork.Categories.Returns(categoryRepository);

            var vm = new CategoriesViewModel(unitOfWork);

            vm.InitializeCommand.Execute(null);

            categoryRepository.Received().GetAll();
        }
        [Fact]
        public void InitializeCommand_Execute_calles_RunnersRepository_GetAll2()
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

            var vm = new CategoriesViewModel(unitOfWork);

            vm.InitializeCommand.Execute(null);

            Assert.NotNull(vm.Categories);
            Assert.NotEmpty(vm.Categories);
            Assert.Equal(3, vm.Categories.Count());
            Assert.Equal(1, vm.Categories.First().Id);
        }
        [Fact]
        public void Categories_setter_raises_propertyChanged_Event()
        {
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var vm = new CategoriesViewModel(unitOfWork);

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
        public void Categories_setter_sets_collection()
        {
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var vm = new CategoriesViewModel(unitOfWork);

            var categories = new List<Category>();
            vm.Categories = categories;

            Assert.Same(categories, vm.Categories);
        }
    }
}
