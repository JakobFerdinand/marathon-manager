using CsvHelper;
using CsvHelper.Configuration;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using UI.ExportResults.CsvMappings;
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
                var (maenner, frauen) = runnerService.GetResultsForCategory(c.Id);
                CreateFile(maenner, Path.Combine(path, $"{c.Name.Replace(" ", "")}_Maenner.csv"));
                CreateFile(frauen, Path.Combine(path, $"{c.Name.Replace(" ", "")}_Frauen.csv"));
            }

            var (aeltesteMaenner, aeltesteFrauen) = runnerService.GetOldestRunner();
            CreateFile<ExportRunner, ExportOldestRunnerMap>(aeltesteMaenner, Path.Combine(path, $"Aelteste_Maenner.csv"));
            CreateFile<ExportRunner, ExportOldestRunnerMap>(aeltesteFrauen, Path.Combine(path, $"Aelteste_Frauen.csv"));

            var vereine = runnerService.GetSportclubsRangs();
            CreateFile(vereine, Path.Combine(path, $"Vereine.csv"));
            var angemeldeteVereine = runnerService.GetAngemeldeteSportclubsRangs();
            CreateFile(angemeldeteVereine, Path.Combine(path, $"AngemeldeteLaeufer_Vereine.csv"));

            var alleLäufer = runnerService.GetAllRunners();
            CreateFile<ExportRunnerSimple, ExportRunnerSimpleMap>(alleLäufer, Path.Combine(path, $"Alle_Laeufer.csv"));
            CreateFile<ExportRunnerSimple, ExportRunnerSimpleMap>(alleLäufer.OrderBy(r => r.Startnummer), Path.Combine(path, $"Alle_Laeufer_nach_Startnummer.csv"));

            string readPath()
            {
                Write("Export Directory Path: ");
                return ReadLine();
            } 
        }

        private static void CreateFile<T>(IEnumerable<T> data, string exportPath)
        {
            using (var streamWriter = new StreamWriter(exportPath, false, Encoding.UTF8))
            using (var csv = new CsvWriter(streamWriter))
            {
                csv.Configuration.Delimiter = ";";
                csv.Configuration.RegisterClassMap<ExportRunnerMap>();
                csv.Configuration.RegisterClassMap<ExportSportsclubMap>();

                csv.WriteRecords(data);
                csv.Flush();
            }
        }

        private static void CreateFile<T, TMap>(IEnumerable<T> data, string exportPath)
            where TMap : ClassMap<T>
        {
            using (var streamWriter = new StreamWriter(exportPath, false, Encoding.UTF8))
            using (var csv = new CsvWriter(streamWriter))
            {
                csv.Configuration.Delimiter = ";";
                csv.Configuration.RegisterClassMap<TMap>();

                csv.WriteRecords(data);
                csv.Flush();
            }
        }
    }
}
