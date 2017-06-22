using Logic.Common.Interfaces;
using Logic.Common.Services;
using StructureMap;

namespace Logic.DIConfiguration
{
    internal class CommonRegistry : Registry
    {
        public CommonRegistry()
        {
            For<IContainer>()
                .Use<Container>()
                .Singleton();

            For<IReader>()
                .Use<ConsoleReader>()
                .Singleton();

            For<IDateTimeManager>()
                .Use<DateTimeManager>()
                .Singleton();
        }
    }
}
