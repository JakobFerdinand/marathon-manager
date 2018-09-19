using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Reflection;

namespace UI.ExportResults
{
    class Program
    {
        private static IConfigurationRoot Configuration { get; set; }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        public static void Startup()
        {
            var basePath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var builder = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            Configuration = builder.Build();
        }
    }
}
