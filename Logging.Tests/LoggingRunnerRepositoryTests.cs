using Core.Models;
using Core.Repositories;
using Logging.Interfaces;
using Logic.Common.Interfaces;
using NSubstitute;
using System;
using Xunit;

namespace Logging.Tests
{
    public class LoggingRunnerRepositoryTests
    {
        [Fact]
        [Trait("Unit", "")]
        public void Constructor_all_parameters_null_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new LoggingRunnerRepository(null, null, null));
        }
        [Fact]
        [Trait("Unit", "")]
        public void Constructor_dateTimeManager_null_ArgumentNullException()
        {
            var logger = Substitute.For<ILogger>();
            var repository = Substitute.For<IRunnerRepository>();

            Assert.Throws<ArgumentNullException>(() => new LoggingRunnerRepository(null, logger, repository));
        }
        [Fact]
        [Trait("Unit", "")]
        public void Constructor_logger_null_ArgumentNullException()
        {
            var dateTimeManager = Substitute.For<IDateTimeManager>();
            var repository = Substitute.For<IRunnerRepository>();

            Assert.Throws<ArgumentNullException>(() => new LoggingRunnerRepository(dateTimeManager, null, repository));
        }
        [Fact]
        [Trait("Unit", "")]
        public void Constructor_repository_null_ArgumentNullException()
        {
            var dateTimeManager = Substitute.For<IDateTimeManager>();
            var logger = Substitute.For<ILogger>();
            var repository = Substitute.For<IRunnerRepository>();

            Assert.Throws<ArgumentNullException>(() => new LoggingRunnerRepository(dateTimeManager, logger, null));
        }
        [Fact]
        [Trait("Unit", "")]
        public void CanCreateInstance()
        {
            var dateTimeManager = Substitute.For<IDateTimeManager>();
            var logger = Substitute.For<ILogger>();
            var repository = Substitute.For<IRunnerRepository>();

            var decorator = new LoggingRunnerRepository(dateTimeManager, logger, repository);

            Assert.NotNull(decorator);
        }
        [Fact]
        [Trait("Unit", "")]
        public void Get_calles_repository_Get()
        {
            var dateTimeManager = Substitute.For<IDateTimeManager>();
            var logger = Substitute.For<ILogger>();
            var repository = Substitute.For<IRunnerRepository>();

            var decorator = new LoggingRunnerRepository(dateTimeManager, logger, repository);

            decorator.Get(4567);

            repository.Received().Get(4567);
        }
        [Fact]
        [Trait("Unit", "")]
        public void GetIfHasNoTimeWithCategory_calles_repository_Get()
        {
            var dateTimeManager = Substitute.For<IDateTimeManager>();
            var logger = Substitute.For<ILogger>();
            var repository = Substitute.For<IRunnerRepository>();

            var decorator = new LoggingRunnerRepository(dateTimeManager, logger, repository);

            decorator.GetIfHasNoTimeWithCategory("1234567890");

            repository.Received().GetIfHasNoTimeWithCategory("1234567890");
        }
        [Fact]
        [Trait("Unit", "")]
        public void GetAll_calles_repository_Get()
        {
            var dateTimeManager = Substitute.For<IDateTimeManager>();
            var logger = Substitute.For<ILogger>();
            var repository = Substitute.For<IRunnerRepository>();

            var decorator = new LoggingRunnerRepository(dateTimeManager, logger, repository);

            decorator.GetAll();

            repository.Received().GetAll();
        }
        [Fact]
        [Trait("Unit", "")]
        public void FirstOrDefault_calles_repository_Get()
        {
            var dateTimeManager = Substitute.For<IDateTimeManager>();
            var logger = Substitute.For<ILogger>();
            var repository = Substitute.For<IRunnerRepository>();

            var decorator = new LoggingRunnerRepository(dateTimeManager, logger, repository);

            decorator.FirstOrDefault(_ => true);

            repository.ReceivedWithAnyArgs().FirstOrDefault(_ => true);
        }
        [Fact]
        [Trait("Unit", "")]
        public void SingleOrDefault_calles_repository_Get()
        {
            var dateTimeManager = Substitute.For<IDateTimeManager>();
            var logger = Substitute.For<ILogger>();
            var repository = Substitute.For<IRunnerRepository>();

            var decorator = new LoggingRunnerRepository(dateTimeManager, logger, repository);

            decorator.SingleOrDefault(_ => true);

            repository.ReceivedWithAnyArgs().SingleOrDefault(_ => true);
        }
        [Fact]
        [Trait("Unit", "")]
        public void Add_calles_repository_Get()
        {
            var dateTimeManager = Substitute.For<IDateTimeManager>();
            var logger = Substitute.For<ILogger>();
            var repository = Substitute.For<IRunnerRepository>();

            var decorator = new LoggingRunnerRepository(dateTimeManager, logger, repository);
            var runner = new Runner();

            decorator.Add(runner);

            repository.Received().Add(runner);
        }
        [Fact]
        [Trait("Unit", "")]
        public void AddRange_calles_repository_Get()
        {
            var dateTimeManager = Substitute.For<IDateTimeManager>();
            var logger = Substitute.For<ILogger>();
            var repository = Substitute.For<IRunnerRepository>();

            var decorator = new LoggingRunnerRepository(dateTimeManager, logger, repository);
            var runners = new[] { new Runner(), new Runner(), new Runner() };

            decorator.AddRange(runners);

            repository.Received().AddRange(runners);
        }
        [Fact]
        [Trait("Unit", "")]
        public void Romove_calles_repository_Get()
        {
            var dateTimeManager = Substitute.For<IDateTimeManager>();
            var logger = Substitute.For<ILogger>();
            var repository = Substitute.For<IRunnerRepository>();

            var decorator = new LoggingRunnerRepository(dateTimeManager, logger, repository);
            var runner = new Runner();

            decorator.Remove(runner);

            repository.Received().Remove(runner);
        }
        [Fact]
        [Trait("Unit", "")]
        public void RemoveRange_calles_repository_Get()
        {
            var dateTimeManager = Substitute.For<IDateTimeManager>();
            var logger = Substitute.For<ILogger>();
            var repository = Substitute.For<IRunnerRepository>();

            var decorator = new LoggingRunnerRepository(dateTimeManager, logger, repository);
            var runners = new[] { new Runner(), new Runner(), new Runner() };

            decorator.RemoveRange(runners);

            repository.Received().RemoveRange(runners);
        }
    }
}
