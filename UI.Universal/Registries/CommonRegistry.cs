using Logic.Common.Interfaces;
using Logic.Common.Services;
using StructureMap;
using UI.Universal.Interfaces;
using UI.Universal.Services;

namespace UI.Universal.Registries
{
    internal class CommonRegistry : Registry
    {
        public CommonRegistry()
        {
            For<IDateTimeManager>()
               .Use<DateTimeManager>()
               .Singleton();

            For<IRunnerService>()
                .Use<RunnerService>()
                .AlwaysUnique();
        }
    }
}
