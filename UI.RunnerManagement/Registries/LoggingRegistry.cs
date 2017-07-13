using Data.Logging;
using Logging.Interfaces;
using Logging.Loggers;
using Logic.Common.Extensions;
using Microsoft.Extensions.Configuration;
using StructureMap;
using System.Collections.Generic;

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
                .Named("ChangesLogger")
                .Singleton();

            For<IChangesLogger>()
                .Use<DbChangesLogger>()
                .Named("DbChangesLogger")
                .AlwaysUnique();

            For<IChangesLogger>()
                .Use<MultiChangesLogger>()
                .Ctor<IEnumerable<IChangesLogger>>()
                .Is(c => new List<IChangesLogger>
                {
                    c.GetInstance<IChangesLogger>("ChangesLogger"),
                    c.GetInstance<IChangesLogger>("DbChangesLogger")
                })
                .AlwaysUnique();
        }
    }
}
