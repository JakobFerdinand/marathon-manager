using Core;
using Core.Repositories;
using Data.Shared;
using Logging.Interfaces;
using System.Linq;

namespace Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RunnerDbContext _context;
        private readonly IChangesFinder _changesFinder;
        private readonly IChangesLogger _changesLogger;

        public ICategoryRepository Categories { get; }
        public IRunnerRepository Runners { get; }

        public UnitOfWork(
            RunnerDbContext context,
            ICategoryRepository categories,
            IRunnerRepository runners,
            IChangesFinder changesFinder,
            IChangesLogger changesLogger)
        {
            _context = context;
            _changesFinder = changesFinder;
            _changesLogger = changesLogger;

            Categories = categories;
            Runners = runners;
        }
        public UnitOfWork(
           RunnerDbContext context,
           ICategoryRepository categories,
           IRunnerRepository runners)
        {
            _context = context;
            _changesFinder = null;
            _changesLogger = null;

            Categories = categories;
            Runners = runners;
        }

        public void Complete()
        {
            if (!_context.ChangeTracker.HasChanges())
                return;
            var changes = _changesFinder?.GetChanges(_context).ToList();

            _context.SaveChanges();
            _changesLogger?.LogChanges(changes);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
