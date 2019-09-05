using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using UI.RunnerManagement;

namespace MarathonManager
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Environment.CurrentDirectory)
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

                var app = new App(builder.Build());
                LogInfo($"{Dns.GetHostName()} is starting MarathonManager.");
                app.Run();
            }
            catch (Exception e)
            {
                LogError(e);
            }
            Console.ReadLine();
        }

        private static void LogInfo(string message)
            => Telemetry.Instance.TrackEvent(message);

        private static void LogError(Exception ex)
            => Telemetry.Instance.TrackException(ex);
    }
}
