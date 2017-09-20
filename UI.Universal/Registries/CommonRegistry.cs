using StructureMap;
using UI.Universal.Interfaces;
using UI.Universal.Services;

namespace UI.Universal.Registries
{
    internal class CommonRegistry : Registry
    {
        public CommonRegistry()
        {
            For<IRunnerService>()
                .Use<RunnerService>()
                .AlwaysUnique();
        }
    }
}
