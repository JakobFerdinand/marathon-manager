using Core.Models;
using Logging.Interfaces;
using Logging.Loggers;
using NSubstitute;
using System;
using Xunit;

namespace Logging.Tests.Loggers
{
    public class MultiChangesLoggerTests
    {
        [Fact]
        [Trait("Unit", "")]
        public void Constructor_loggers_null_ArgumentNullException() => Assert.Throws<ArgumentNullException>(() => new MultiChangesLogger(null));
        [Fact]
        [Trait("Unit", "")]
        public void CanCreateInstance()
        {
            var loggers = new IChangesLogger[0];
            var multiChangesLogger = new MultiChangesLogger(loggers);
            Assert.NotNull(multiChangesLogger);
        }
        [Fact]
        [Trait("Unit", "")]
        public void LogChanges_calles_LogChanges_on_each_logger()
        {
            var loggers = new []
            {
                Substitute.For<IChangesLogger>(),
                Substitute.For<IChangesLogger>(),
                Substitute.For<IChangesLogger>()
            };

            var multiChangesLogger = new MultiChangesLogger(loggers);

            var changes = new[]
            {
                new ChangeLog { Id = 432 },
                new ChangeLog { Id = 236 },
                new ChangeLog { Id = 14 },
                new ChangeLog { Id = 53 },
                new ChangeLog { Id = 356 },
                new ChangeLog { Id = 343 },
                new ChangeLog { Id = 6745 }
            };

            multiChangesLogger.LogChanges(changes);

            loggers.ForEach(l => l.Received().LogChanges(changes));
        }
    }
}
