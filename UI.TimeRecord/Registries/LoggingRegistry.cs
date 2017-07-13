using Logging.Interfaces;
using Logging.Loggers;
using Logic.Common.Extensions;
using Microsoft.Extensions.Configuration;
using StructureMap;
using System.Collections.Generic;

namespace UI.TimeRecord.Registries
{
    internal class LoggingRegistry : Registry
    {
        public LoggingRegistry(IConfigurationRoot configuration)
        {
            For<ILogger>()
                .Use<ConsoleLogger>()
                .Named("ConsoleLogger")
                .Singleton();

            For<ILogger>()
                .Use<HtmlFileLogger>()
                .Named("HtmlLogger")
                .Ctor<string>()
                .Is(configuration.GetLoggingPath("HtmlFilePath"))
                .Singleton();

            For<ILogger>()
                .Use<MultiLogger>()
                .Ctor<IEnumerable<ILogger>>()
                .Is(c => new List<ILogger>
                {
                    c.GetInstance<ILogger>("ConsoleLogger"),
                    c.GetInstance<ILogger>("HtmlLogger")
                });

            For<IChangesLogger>()
                .Use<ChangesLogger>()
                .Ctor<ILogger>()
                .Is(c => c.GetInstance<ILogger>("HtmlLogger"))
                .Singleton();
        }
    }
}
