using Core.Extensions;
using Core.Models;
using Logging.Interfaces;
using System.Collections.Generic;

namespace Logging.Loggers
{
    public class MultiChangesLogger : IChangesLogger
    {
        private readonly IEnumerable<IChangesLogger> _loggers;

        public MultiChangesLogger(IEnumerable<IChangesLogger> loggers) => _loggers = loggers;

        public void LogChanges(IEnumerable<ChangeLog> changes) => _loggers.ForEach(l => l.LogChanges(changes));
    }
}
