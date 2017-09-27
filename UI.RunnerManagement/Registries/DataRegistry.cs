using Core;
using Core.Repositories;
using Data;
using Data.Repositories;
using Data.Shared;
using Logging;
using StructureMap;

namespace UI.RunnerManagement.Registries
{
    internal class DataRegistry : Registry
    {
        public DataRegistry(bool useSampleData)
        {
            if (useSampleData)
            {
                For<ICategoryRepository>()
                    .Use<Data.Sample.Repositories.CategoryRepository>()
                    .AlwaysUnique();

                For<IRunnerRepository>()
                    .Use<Data.Sample.Repositories.RunnerRepository>()
                    .AlwaysUnique();

                For<IUnitOfWork>()
                    .Use<Data.Sample.UnitOfWork>()
                    .AlwaysUnique();
            }
            else
            {
                For<ICategoryRepository>()
                    .Use<CategoryRepository>()
                    .AlwaysUnique();

                For<IRunnerRepository>()
                    .DecorateAllWith<LoggingRunnerRepository>();

                For<IRunnerRepository>()
                    .Use<RunnerRepository>()
                    .AlwaysUnique();

                For<IChangesFinder>()
                    .Use<ChangesFinder>()
                    .Singleton();

                For<IUnitOfWork>()
                    .Use<UnitOfWork>()
                    .AlwaysUnique();
            }
        }
    }
}
