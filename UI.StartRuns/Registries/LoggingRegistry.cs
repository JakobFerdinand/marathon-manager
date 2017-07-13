using Logging.Interfaces;
using Logging.Loggers;
using Logic.Common.Extensions;
using Microsoft.Extensions.Configuration;
using StructureMap;

namespace UI.StartRuns.Registries
{
    internal class LoggingRegistry : Registry
    {
        public LoggingRegistry(IConfigurationRoot configuration)
        {
            For<ILogger>()
                .Use<HtmlFileLogger>()
                .Ctor<string>()
                .Is(configuration.GetLoggingPath("HtmlFilePath"))
                .Singleton();

            For<IChangesLogger>()
                .Use<ChangesLogger>()
                .Singleton();
        }
    }
}
