using System;
using Core;
using Core.Repositories;

namespace Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RunnersContext _context;

        public ICategoryRepository Categories { get; }
        public IRunnerRepository Runners { get; }

        public UnitOfWork(
            RunnersContext context,
            ICategoryRepository categories,
            IRunnerRepository runners)
        {
            _context = context;

            Categories = categories;
            Runners = runners;
        }

        public void Complete()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
