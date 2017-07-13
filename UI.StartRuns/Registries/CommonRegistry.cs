using Logic.Common.Interfaces;
using Logic.Common.Services;
using StructureMap;

namespace UI.StartRuns.Registries
{
    internal class CommonRegistry : Registry
    {
        public CommonRegistry()
        {
            For<IDateTimeManager>()
                .Use<DateTimeManager>()
                .Singleton();
        }
    }
}
