using Core;
using Core.Repositories;
using Data;
using Data.Repositories;
using Logic.Common.Decorators;
using Microsoft.EntityFrameworkCore;
using StructureMap;

namespace Logic.DIConfiguration
{
    internal class DataRegistry : Registry
    {
        public DataRegistry()
        {
            ForConcreteType<RunnersContext>()
                .Configure
                .Ctor<DbContextOptions<RunnersContext>>()
                .Is(SqlOptionsBuilder.GetOptions());

            For<ICategoryRepository>()
                .Use<CategoryRepository>()
                .AlwaysUnique();

            For<IRunnerRepository>()
                .DecorateAllWith<LoggingRunnerRepository>();

            For<IRunnerRepository>()
                .Use<RunnerRepository>()
                .AlwaysUnique();

            For<IUnitOfWork>()
                .Use<UnitOfWork>()
                .AlwaysUnique();
        }
    }
}
