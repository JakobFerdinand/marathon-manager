using Logging.Interfaces;
using Logging.Loggers;
using StructureMap;
using System.Collections.Generic;

namespace Logic.DIConfiguration
{
    internal class LoggingRegistry : Registry
    {
        public LoggingRegistry()
        {
            For<ILogger>()
                .Use<ConsoleLogger>()
                .Named("ConsoleLogger")
                .Singleton();

            For<ILogger>()
                .Use<HtmlFileLogger>()
                .Named("HtmlFileLogger")
                .Ctor<string>()
                .Is(@"C:\Logs\MarathonManagerTimeRecordLog.html")
                .Singleton();

            For<ILogger>()
                .Use<MultiLogger>()
                .Ctor<IEnumerable<ILogger>>()
                .Is(c => new [] 
                {
                    c.GetInstance<ILogger>("ConsoleLogger"),
                    c.GetInstance<ILogger>("HtmlFileLogger")
                })
                .Singleton();
        }
    }
}
