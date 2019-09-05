using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
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
                LogInfo($"MarathonManager starting", Enumerable.Range(0, 1).ToImmutableDictionary(_ => "DnsHostName", _ => Dns.GetHostName()));
                app.Run();
            }
            catch (Exception e)
            {
                LogError(e);
            }
            Telemetry.Instance.Flush();
        }

        private static void LogInfo(string key, IDictionary<string, string> properties = null)
            => Telemetry.Instance.TrackEvent(key, properties);

        private static void LogError(Exception ex)
            => Telemetry.Instance.TrackException(ex);
    }
}
