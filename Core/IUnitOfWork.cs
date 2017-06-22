using Core.Repositories;
using System;

namespace Core
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository Categories { get; }
        IRunnerRepository Runners { get; }

        void Complete();
    }
}
