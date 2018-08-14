using Core.Decorators;
using Core.Models;
using Core.Repositories;
using NSubstitute;
using System;
using System.Linq.Expressions;
using Xunit;

namespace Core.Tests.Decorators
{
    public class RunnerRepositoryDecoratorTests
    {
        [Fact]
        [Trait("Unit", "")]
        public void Constructor_BaseRepository_null_ArgumentNullException() => Assert.Throws<ArgumentNullException>(() => new EmptyRunnerRepositoryDecoratorSubClass(null));
        [Fact]
        [Trait("Unit", "")]
        public void CanCreateInstance()
        {
            var baseRepository = Substitute.For<IRunnerRepository>();
            var dec = new EmptyRunnerRepositoryDecoratorSubClass(baseRepository);
            Assert.NotNull(dec);
        }
        [Fact]
        [Trait("Unit", "")]
        public void Get8_Calles_BaseRepository_Get8()
        {
            var baseRepository = Substitute.For<IRunnerRepository>();
            var dec = new EmptyRunnerRepositoryDecoratorSubClass(baseRepository);

            dec.Get(8);

            baseRepository.Received().Get(8);
        }
        [Fact]
        [Trait("Unit", "")]
        public void GetMAX_Calles_BaseRepository_GetMAX()
        {
            var baseRepository = Substitute.For<IRunnerRepository>();
            var dec = new EmptyRunnerRepositoryDecoratorSubClass(baseRepository);

            dec.Get(int.MaxValue);

            baseRepository.Received().Get(int.MaxValue);
        }
        [Fact]
        [Trait("Unit", "")]
        public void GetAlltrue_Calles_BaseRepository_GetAlltrue()
        {
            var baseRepository = Substitute.For<IRunnerRepository>();
            var dec = new EmptyRunnerRepositoryDecoratorSubClass(baseRepository);

            dec.GetAll(true);

            baseRepository.Received().GetAll(true);
        }
        [Fact]
        [Trait("Unit", "")]
        public void GetAllfalse_Calles_BaseRepository_GetAllfalse()
        {
            var baseRepository = Substitute.For<IRunnerRepository>();
            var dec = new EmptyRunnerRepositoryDecoratorSubClass(baseRepository);

            dec.GetAll(false);

            baseRepository.Received().GetAll(false);
        }
        [Fact]
        [Trait("Unit", "")]
        public void GetAllWithRelatedtrue_Calles_BaseRepository_GetAllWithRelatedtrue()
        {
            var baseRepository = Substitute.For<IRunnerRepository>();
            var dec = new EmptyRunnerRepositoryDecoratorSubClass(baseRepository);

            dec.GetAllWithCategories(true);

            baseRepository.Received().GetAllWithCategories(true);
        }
        [Fact]
        [Trait("Unit", "")]
        public void GetAllWithRelatedfalse_Calles_BaseRepository_GetAllWithRelatedfalse()
        {
            var baseRepository = Substitute.For<IRunnerRepository>();
            var dec = new EmptyRunnerRepositoryDecoratorSubClass(baseRepository);

            dec.GetAllWithCategories(false);

            baseRepository.Received().GetAllWithCategories(false);
        }
        [Fact]
        [Trait("Unit", "")]
        public void Count_Calles_BaseRepository_Count()
        {
            var baseRepository = Substitute.For<IRunnerRepository>();
            var dec = new EmptyRunnerRepositoryDecoratorSubClass(baseRepository);

            Expression<Func<Runner, bool>> predicate = _ => false;

            dec.Count(predicate);

            baseRepository.ReceivedWithAnyArgs().Count(null);
            baseRepository.Received().Count(predicate);
        }
        [Fact]
        [Trait("Unit", "")]
        public void Find_Calles_BaseRepository_Find()
        {
            var baseRepository = Substitute.For<IRunnerRepository>();
            var dec = new EmptyRunnerRepositoryDecoratorSubClass(baseRepository);

            Expression<Func<Runner, bool>> predicate = _ => false;

            dec.Find(predicate);

            baseRepository.ReceivedWithAnyArgs().Find(null);
            baseRepository.Received().Find(predicate);
        }
        [Fact]
        [Trait("Unit", "")]
        public void FirstOrDefault_Calles_BaseRepository_FirstOrDefault()
        {
            var baseRepository = Substitute.For<IRunnerRepository>();
            var dec = new EmptyRunnerRepositoryDecoratorSubClass(baseRepository);

            Expression<Func<Runner, bool>> predicate = _ => false;

            dec.FirstOrDefault(predicate);

            baseRepository.ReceivedWithAnyArgs().FirstOrDefault(null);
            baseRepository.Received().FirstOrDefault(predicate);
        }
        [Fact]
        [Trait("Unit", "")]
        public void SingleOrDefault_Calles_BaseRepository_SingleOrDefault()
        {
            var baseRepository = Substitute.For<IRunnerRepository>();
            var dec = new EmptyRunnerRepositoryDecoratorSubClass(baseRepository);

            Expression<Func<Runner, bool>> predicate = _ => false;

            dec.SingleOrDefault(predicate);

            baseRepository.ReceivedWithAnyArgs().SingleOrDefault(null);
            baseRepository.Received().SingleOrDefault(predicate);
        }
        [Fact]
        [Trait("Unit", "")]
        public void Add_Calles_BaseRepository_Add()
        {
            var baseRepository = Substitute.For<IRunnerRepository>();
            var dec = new EmptyRunnerRepositoryDecoratorSubClass(baseRepository);

            var runner = new Runner { Id = 68430 };

            dec.Add(runner);

            baseRepository.ReceivedWithAnyArgs().Add(null);
            baseRepository.Received().Add(runner);
        }
        [Fact]
        [Trait("Unit", "")]
        public void AddRange_Calles_BaseRepository_AddRange()
        {
            var baseRepository = Substitute.For<IRunnerRepository>();
            var dec = new EmptyRunnerRepositoryDecoratorSubClass(baseRepository);

            var runners = new[]
            {
                new Runner { Id = 68430 },
                new Runner { Id = 356 },
            };

            dec.AddRange(runners);

            baseRepository.ReceivedWithAnyArgs().AddRange(null);
            baseRepository.Received().AddRange(runners);
        }
        [Fact]
        [Trait("Unit", "")]
        public void Remove_Calles_BaseRepository_Remove()
        {
            var baseRepository = Substitute.For<IRunnerRepository>();
            var dec = new EmptyRunnerRepositoryDecoratorSubClass(baseRepository);

            var runner = new Runner { Id = 68430 };

            dec.Remove(runner);

            baseRepository.ReceivedWithAnyArgs().Remove(null);
            baseRepository.Received().Remove(runner);
        }
        [Fact]
        [Trait("Unit", "")]
        public void RemoveRange_Calles_BaseRepository_RemoveRange()
        {
            var baseRepository = Substitute.For<IRunnerRepository>();
            var dec = new EmptyRunnerRepositoryDecoratorSubClass(baseRepository);

            var runners = new[]
            {
                new Runner { Id = 68430 },
                new Runner { Id = 356 },
            };

            dec.RemoveRange(runners);

            baseRepository.ReceivedWithAnyArgs().RemoveRange(null);
            baseRepository.Received().RemoveRange(runners);
        }

        class EmptyRunnerRepositoryDecoratorSubClass : RunnerRepositoryDecorator
        {
            public EmptyRunnerRepositoryDecoratorSubClass(IRunnerRepository baseRepository) : base(baseRepository)
            { }
        }
    }
}
