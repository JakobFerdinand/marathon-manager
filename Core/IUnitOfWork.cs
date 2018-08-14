using Core.Repositories;
using System;

namespace Core
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository Categories { get; }
        IRunnerRepository Runners { get; }
        void Attach<T>(T runner) where T : class;
        bool HasChanges();

        void Complete();
    }
}
