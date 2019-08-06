using Core;
using Core.Repositories;
using Data.Sample.Repositories;

namespace Data.Sample
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICategoryRepository Categories { get; }
        public IRunnerRepository Runners { get; }

        public IDatabase Database { get; }

        public UnitOfWork()
        {
            var categories = new CategoryRepository();
            Categories = categories;
            Runners = new RunnerRepository(categories);
            Database = new Database();
        }

        public void Complete()
        { }

        public void Dispose()
        { }

        public bool HasChanges() => false;

        public void Attach<T>(T runner) where T : class
            => throw new System.NotImplementedException();
    }
}
