using Data.Logging;
using Logging.Interfaces;
using Logic.Common.Extensions;
using Logging.Loggers;
using Microsoft.Extensions.Configuration;
using StructureMap;

namespace UI.RunnerManagement.Registries
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

            //For<IChangesLogger>()
            //    .Use<DbChangesLogger>()
            //    .AlwaysUnique();
        }
    }
}
