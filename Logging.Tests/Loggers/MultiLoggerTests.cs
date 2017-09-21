using Core.Extensions;
using Logging.Interfaces;
using Logging.Loggers;
using NSubstitute;
using System;
using Xunit;

namespace Logging.Tests.Loggers
{
    public class MultiLoggerTests
    {
        [Fact]
        public void Constructor_loggers_null_ArgumentNullException() => Assert.Throws<ArgumentNullException>(() => new MultiLogger(null));
        [Fact]
        public void CanCreateInstance()
        {
            var loggers = new ILogger[0];
            var multiLogger = new MultiLogger(loggers);
            Assert.NotNull(multiLogger);
        }
        [Fact]
        public void LogError_calles_LogError_on_each_logger()
        {
            var loggers = new[]
            {
                Substitute.For<ILogger>(),
                Substitute.For<ILogger>(),
                Substitute.For<ILogger>()
            };

            var multiLogger = new MultiLogger(loggers);

            multiLogger.LogError("Error to Log");

            loggers.ForEach(l => l.Received().LogError("Error to Log"));
            loggers.ForEach(l => l.DidNotReceiveWithAnyArgs().LogMessage(null));
            loggers.ForEach(l => l.DidNotReceiveWithAnyArgs().LogSuccess(null));
        }
        [Fact]
        public void LogMessage_calles_LogMessage_on_each_logger()
        {
            var loggers = new[]
            {
                Substitute.For<ILogger>(),
                Substitute.For<ILogger>(),
                Substitute.For<ILogger>()
            };

            var multiLogger = new MultiLogger(loggers);

            multiLogger.LogMessage("Message to Log");

            loggers.ForEach(l => l.Received().LogMessage("Message to Log"));
            loggers.ForEach(l => l.DidNotReceiveWithAnyArgs().LogError(null));
            loggers.ForEach(l => l.DidNotReceiveWithAnyArgs().LogSuccess(null));
        }
        [Fact]
        public void LogSuccess_calles_LogSuccess_on_each_logger()
        {
            var loggers = new[]
            {
                Substitute.For<ILogger>(),
                Substitute.For<ILogger>(),
                Substitute.For<ILogger>()
            };

            var multiLogger = new MultiLogger(loggers);

            multiLogger.LogSuccess("Success to Log");

            loggers.ForEach(l => l.Received().LogSuccess("Success to Log"));
            loggers.ForEach(l => l.DidNotReceiveWithAnyArgs().LogError(null));
            loggers.ForEach(l => l.DidNotReceiveWithAnyArgs().LogMessage(null));
        }
    }
}
