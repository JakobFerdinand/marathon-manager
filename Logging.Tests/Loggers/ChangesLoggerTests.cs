using Core.Models;
using Logging.Interfaces;
using Logging.Loggers;
using NSubstitute;
using System;
using Xunit;

namespace Logging.Tests.Loggers
{
    public class ChangesLoggerTests
    {
        [Fact]
        public void Constructor_Logger_Null_ArgumentNullException() => Assert.Throws<ArgumentNullException>(() => new ChangesLogger(null));
        [Fact]
        public void CanCreateInstance()
        {
            var logger = Substitute.For<ILogger>();
            var changesLogger = new ChangesLogger(logger);
            Assert.NotNull(changesLogger);
        }
        [Fact]
        public void LogChanges_with_3_changes_Calles_Logger_LogMessage_3_times()
        {
            var logger = Substitute.For<ILogger>();
            var changesLogger = new ChangesLogger(logger);

            var changes = new[]
            {
                new ChangeLog { Id = 843 },
                new ChangeLog { Id = 83 },
                new ChangeLog { Id = 425 },
            };

            changesLogger.LogChanges(changes);

            logger.ReceivedWithAnyArgs(3).LogMessage(null);
        }
    }
}
