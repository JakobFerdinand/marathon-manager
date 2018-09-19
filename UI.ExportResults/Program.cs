using CsvHelper;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Reflection;
using UI.ExportResults.Models;
using UI.ExportResults.Services;
using static System.Console;

namespace UI.ExportResults
{
    internal class Program
    {
        private static IConfigurationRoot Configuration { get; set; }

        private static void Main(string[] args)
        {
            Startup();
            Export();
        }

        public static void Startup()
        {
            var basePath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var builder = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            Configuration = builder.Build();
        }

        private static void Export()
        {
            var path = string.Empty;
            while (!Directory.Exists(path))
                path = readPath();

            var context = new RunnerDbContext(new DbContextOptionsBuilder<RunnerDbContext>()
                .UseSqlServer(Configuration.GetConnectionString("Default"))
                .Options);
            var runnerService = new RunnerService(context);

            var categories = context.Categories.ToImmutableList();
            foreach (var c in categories)
            {
                var (m, f) = runnerService.GetResultsForCategory(c.Id);
                WriteRunners(m, Path.Combine(path, $"{c.Name.Replace(" ", "")}_Maenner.csv"));
                WriteRunners(f, Path.Combine(path, $"{c.Name.Replace(" ", "")}_Frauen.csv"));

                if (c.Name.StartsWith("Hobbylauf"))
                {
                    var (jugendMaenner, jugendFrauen) = runnerService.GetResultsForCategory(c.Id, runnerBornInOrAfterYear: 2007);
                    WriteRunners(jugendMaenner, Path.Combine(path, $"{c.Name.Replace(" ", "")}_Jugend_Maenner.csv"));
                    WriteRunners(jugendFrauen, Path.Combine(path, $"{c.Name.Replace(" ", "")}_Jugend_Frauen.csv"));
                }
            }

            var (maenner, frauen) = runnerService.GetOldestRunner();
            WriteRunners(maenner, Path.Combine(path, $"Aelteste_Maenner.csv"));
            WriteRunners(frauen, Path.Combine(path, $"Aelteste_Frauen.csv"));

            string readPath()
            {
                Write("Export Directory Path: ");
                return ReadLine();
            }
        }

        private static void WriteRunners(IEnumerable<ExportRunner> runners, string exportPath)
        {
            using (var streamWriter = new StreamWriter(exportPath))
            using (var csv = new CsvWriter(streamWriter))
            {
                csv.Configuration.Delimiter = ";";

                csv.WriteRecords(runners);
                csv.Flush();
            }
        }
    }
}
