using Core.Extensions;
using Core.Models;
using Logging.Interfaces;
using System;
using System.Collections.Generic;

namespace Logging.Loggers
{
    public class MultiChangesLogger : IChangesLogger
    {
        private readonly IEnumerable<IChangesLogger> _loggers;

        public MultiChangesLogger(IEnumerable<IChangesLogger> loggers) => _loggers = loggers ?? throw new ArgumentNullException(nameof(loggers), $"{nameof(loggers)} must not be null.");

        public void LogChanges(IEnumerable<ChangeLog> changes) => _loggers.ForEach(l => l.LogChanges(changes));
    }
}
