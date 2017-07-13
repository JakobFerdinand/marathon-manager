using Core.Models;
using Logging.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Logging.Loggers
{
    public class ChangesLogger : IChangesLogger
    {
        private readonly ILogger _logger;

        public ChangesLogger(ILogger logger)
        {
            _logger = logger;
        }

        public void LogChanges(IEnumerable<ChangeLog> changes)
        {
            foreach (var c in changes)
                _logger.LogMessage($"{c.ChangeTime.ToString("yyyy.MM.dd - HH.mm.ss.fff")} | {c.TypeName} | Id: {c.Id} | {c.PropertyName} | OldValue: {c.OldValue} | NewValue: {c.NewValue}");
        }
    }
}
