using Core;
using Core.Repositories;
using Data;
using Data.Repositories;
using Data.Shared;
using Logic.Common.Decorators;
using StructureMap;

namespace UI.StartRuns.Registries
{
    internal class DataRegistry : Registry
    {
        public DataRegistry()
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
