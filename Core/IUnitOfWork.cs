using Core.Repositories;
using System;

namespace Core
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository Categories { get; }
        IRunnerRepository Runners { get; }
        IDatabase Database { get; }

        void Attach<T>(T entity) where T : class;
        bool HasChanges();

        void Complete();
    }
}
