using Logging.Interfaces;
using System;
using System.Collections.Generic;

namespace Logging.Loggers
{
    public class MultiLogger : ILogger
    {
        private readonly IEnumerable<ILogger> _loggers;

        public MultiLogger(IEnumerable<ILogger> loggers) => _loggers = loggers ?? throw new ArgumentNullException(nameof(loggers), $"{nameof(loggers)} must not be null.");

        public void LogError(string message) => _loggers.ForEach(l => l.LogError(message));
        public void LogMessage(string message) => _loggers.ForEach(l => l.LogMessage(message));
        public void LogSuccess(string message) => _loggers.ForEach(l => l.LogSuccess(message));
    }
}
