using Core.Models;
using Logging.Interfaces;
using System.Collections.Generic;

namespace SampleData
{
    public class EmptyChangesLogger : IChangesLogger
    {
        public void LogChanges(IEnumerable<ChangeLog> changes)
        {
        }
    }
}
