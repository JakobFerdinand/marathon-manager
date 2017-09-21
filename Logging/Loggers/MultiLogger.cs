using Logging.Interfaces;
using System.Collections.Generic;

namespace Logging.Loggers
{
    public class MultiLogger : ILogger
    {
        private readonly IEnumerable<ILogger> _loggers;

        public MultiLogger(IEnumerable<ILogger> loggers) => _loggers = loggers;

        public void LogError(string message)
        {
            foreach (var logger in _loggers)
                logger.LogError(message);
        }

        public void LogMessage(string message)
        {
            foreach (var logger in _loggers)
                logger.LogMessage(message);
        }

        public void LogSuccess(string message)
        {
            foreach (var logger in _loggers)
                logger.LogSuccess(message);
        }
    }
}
