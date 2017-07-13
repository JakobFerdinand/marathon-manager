using Logic.Common.Interfaces;
using Logic.Common.Services;
using StructureMap;

namespace UI.TimeRecord.Registries
{
    internal class CommonRegistry : Registry
    {
        public CommonRegistry()
        {
            For<IReader>()
                .Use<ConsoleReader>()
                .Singleton();

            For<IDateTimeManager>()
                .Use<DateTimeManager>()
                .Singleton();
        }
    }
}
