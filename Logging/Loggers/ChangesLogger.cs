using Core.Extensions;
using Core.Models;
using Logging.Interfaces;
using System;
using System.Collections.Generic;

namespace Logging.Loggers
{
    public class ChangesLogger : IChangesLogger
    {
        private readonly ILogger _logger;

        public ChangesLogger(ILogger logger) => _logger = logger ?? throw new ArgumentNullException(nameof(logger), $"{nameof(logger)} must not be null.");

        public void LogChanges(IEnumerable<ChangeLog> changes) => changes.ForEach(c => 
                _logger.LogMessage($"{c.ChangeTime.ToString("yyyy.MM.dd - HH.mm.ss.fff")} | {c.TypeName} | Id: {c.EntityId} | {c.PropertyName} | OldValue: {c.OldValue} | NewValue: {c.NewValue}"));
    }
}
