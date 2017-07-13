using Core.Models;
using System.Collections.Generic;

namespace Logging.Interfaces
{
    public interface IChangesLogger
    {
        void LogChanges(IEnumerable<ChangeLog> changes);
    }
}
