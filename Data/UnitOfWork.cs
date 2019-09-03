using Core;
using Core.Repositories;
using Data.Shared;
using Logging.Interfaces;
using System;
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
        public IDatabase Database { get; }

        public UnitOfWork(
            RunnerDbContext context,
            ICategoryRepository categories,
            IRunnerRepository runners,
            IDatabase database)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

            Categories = categories ?? throw new ArgumentNullException(nameof(categories));
            Runners = runners ?? throw new ArgumentNullException(nameof(runners));
            Database = database ?? throw new ArgumentNullException(nameof(database));
        }
        public UnitOfWork(
            RunnerDbContext context,
            ICategoryRepository categories,
            IRunnerRepository runners,
            IDatabase database,
            IChangesFinder changesFinder,
            IChangesLogger changesLogger)
            : this(context, categories, runners, database)
        {
            _changesFinder = changesFinder ?? throw new ArgumentNullException(nameof(changesFinder));
            _changesLogger = changesLogger ?? throw new ArgumentNullException(nameof(changesLogger));
        }

        public void Attach<T>(T entity) where T : class
            => _context.Set<T>().Attach(entity);

        public bool HasChanges() => _context.ChangeTracker.HasChanges();

        public void Complete()
        {
            if (!_context.ChangeTracker.HasChanges())
                return;
            var changes = _changesFinder?.GetChanges(_context).ToList();

            _context.SaveChanges();
            _changesLogger?.LogChanges(changes);
        }

        public void Dispose() => _context.Dispose();
    }
}
