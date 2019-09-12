using System;
using Logging.Interfaces;

namespace Logging.Loggers
{
    public class ConsoleLogger : ILogger
    {
        public void LogError(string message) => Log(message, ConsoleColor.Red);
        public void LogMessage(string message) => Log(message);
        public void LogSuccess(string message) => Log(message, ConsoleColor.Green);

        private void Log(string message, ConsoleColor? color = null)
        {
            var currentColor = Console.ForegroundColor;
            if (color != null)
                Console.ForegroundColor = color.Value;

            Console.WriteLine(message);

            Console.ForegroundColor = currentColor;
        }
    }
}
