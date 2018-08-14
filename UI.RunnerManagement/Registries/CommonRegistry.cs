using Logic.Common.Interfaces;
using Logic.Common.Services;
using StructureMap;
using UI.RunnerManagement.Services;

namespace UI.RunnerManagement.Registries
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

            For<IDialogService>()
                .Use<DialogService>()
                .Singleton();
        }
    }
}
