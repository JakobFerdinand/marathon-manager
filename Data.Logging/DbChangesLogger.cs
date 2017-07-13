using Core.Models;
using Logging.Interfaces;
using System.Collections.Generic;

namespace Data.Logging
{
    public class DbChangesLogger : IChangesLogger
    {
        private readonly LoggingDbContext _context;

        public DbChangesLogger(LoggingDbContext context)
        {
            _context = context;
        }

        public void LogChanges(IEnumerable<ChangeLog> changes)
        {
            _context.AddRange(changes);
            _context.SaveChanges();
        }
    }
}
