using Core;
using Core.Repositories;
using Data.Sample.Repositories;

namespace Data.Sample
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICategoryRepository Categories { get; }
        public IRunnerRepository Runners { get; }

        public UnitOfWork()
        {
            var categories = new CategoryRepository();
            Categories = categories;
            Runners = new RunnerRepository(categories);
        }

        public void Complete()
        { }

        public void Dispose()
        { }
    }
}
