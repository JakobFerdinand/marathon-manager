using AutoMapper;
using Core.Models;
using Core.Repositories;
using Data;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using UI.JsonImport.Services;
using static System.Console;

namespace UI.JsonImport
{
    internal class Program
    {
        private static IConfigurationRoot Configuration { get; set; }

        private static void Main(string[] _)
        {
            Startup();

            WriteLine("1. Delete and Create Database");
            WriteLine("2. Insert Categories");
            WriteLine("3. Import Json Runners");
            WriteLine("exit");

            while (true)
            {
                var input = ReadLine();

                switch (input)
                {
                    case "exit":
                        return;

                    case "1":
                        DeleteAndCreateDatabase();
                        break;

                    case "2":
                        InsertCategories();
                        break;

                    case "3":
                        ImportRunners();
                        break;

                    default:
                        WriteLine("Invalid input");
                        break;
                }
            }
        }

        public static void Startup()
        {
            var basePath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var builder = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            Configuration = builder.Build();
        }

        private static void DeleteAndCreateDatabase()
        {
            var options = new DbContextOptionsBuilder<RunnerDbContext>()
                .UseSqlServer(Configuration.GetConnectionString("Default"))
                .Options;
            using (var context = new RunnerDbContext(options))
            {
                if (context.Database.EnsureDeleted())
                    WriteLine("Database was successfully deleted.");

                if (context.Database.EnsureCreated())
                    WriteLine("Database was successfully created.");
            }
        }

        private static void InsertCategories()
        {
            var defaultCategories = ReadCategories();

            var options = new DbContextOptionsBuilder<RunnerDbContext>()
                .UseSqlServer(Configuration.GetConnectionString("Default"))
                .Options;
            using (var context = new RunnerDbContext(options))
            {
                var categories = defaultCategories.Select(c => new Category { Name = c.Name });
                context.AddRange(categories);
                context.SaveChanges();

                WriteLine("Categories were successfully saved:");
                categories.ForEach(c => WriteLine($"| {c.Name}"));
            }
        }

        private static ImmutableArray<Models.Category> ReadCategories()
        {
            var path = GetCategoriesFilePath();

            using (var filereader = new StreamReader(path))
            {
                var json = filereader.ReadToEndClean();
                return JsonConvert
                    .DeserializeObject<IEnumerable<Models.Category>>(json)
                    .ToImmutableArray();
            }
        }

        private static string GetCategoriesFilePath()
        {
            var path = @".\categories.json";
            while (!File.Exists(path))
                path = ReadPath("Categories");
            return path;
        }

        private static void ImportRunners()
        {
            var path = string.Empty;
            while (!File.Exists(path))
                path = ReadPath("Runners");

            var categories = ReadCategories();

            var filereader = new StreamReader(path);
            var json = filereader.ReadToEndClean();

            var options = new DbContextOptionsBuilder<RunnerDbContext>()
                .UseSqlServer(Configuration.GetConnectionString("Default"))
                .Options;
            using (var context = new RunnerDbContext(options))
            {
                var categoryRepository = new CategoryRepository(context);
                var runnerRepository = new RunnerRepository(context);
                var database = new Database(context);
                using (var unitOfWork = new UnitOfWork(context, categoryRepository, runnerRepository, database))
                {
                    var mapperConfiguration = GetMapperConfiguration(unitOfWork.Categories, categories.ToImmutableDictionary(c => c.ShortName, c => c.Name));
                    var mapper = new Mapper(mapperConfiguration);

                    var deserializer = new JsonDeserializer(mapper);

                    var runners = deserializer.Deserialize(json);

                    runnerRepository.AddRange(runners);
                    unitOfWork.Complete();

                    WriteLine($"{runners.Count()} Runners were successfully saved.");
                }
            }
        }

        private static string ReadPath(string fileToSearch)
        {
            Write($"Import {fileToSearch} File Path: ");
            return ReadLine();
        }

        private static AutoMapper.IConfigurationProvider GetMapperConfiguration(ICategoryRepository categoryRepository, ImmutableDictionary<string, string> categoryMapping)
        {
            var categories = categoryRepository.GetAll(asNoTracking: true);

            return new MapperConfiguration(c =>
            {
                c.CreateMap<Models.Runner, Runner>()
                    .ForMember(r => r.Gender, map => map.MapFrom(r => r.Geschlecht == "Mann" ? Gender.Mann : Gender.Frau))
                    .ForMember(r => r.Firstname, map => map.MapFrom(r => r.Vorname))
                    .ForMember(r => r.Lastname, map => map.MapFrom(r => r.Nachname))
                    .ForMember(r => r.YearOfBirth, map => map.MapFrom(r => int.Parse(r.Geburtsdatum.Substring(0, 4))))
                    .ForMember(r => r.City, map => map.MapFrom(r => r.Wohnort))
                    .ForMember(r => r.Email, map => map.MapFrom(r => r.eMail))
                    .ForMember(r => r.SportsClub, map => map.MapFrom(r => r.Verein))
                    .ForMember(r => r.CategoryId, map => map.MapFrom(r => categories.Single(category => category.Name == r.Strecken).Id))
                    .ForMember(r => r.Id, map => map.Ignore())
                    .ForMember(r => r.RunningTime, map => map.Ignore())
                    .ForMember(r => r.Category, map => map.Ignore())
                    .ForMember(r => r.ChipId, map => map.Ignore())
                    .ForMember(r => r.RunningTime, map => map.Ignore())
                    .ForMember(r => r.TimeAtDestination, map => map.Ignore())
                    .ForMember(r => r.Startnumber, map => map.Ignore());

                c.CreateMap<Models.ImportObject, Runner>()
                    .BeforeMap((i, _) => i.Bewerb.CheckCategory(categoryMapping))
                    .ForMember(r => r.Gender, map => map.MapFrom(r => r.Geschlecht == "Geschl_M" ? Gender.Mann : Gender.Frau))
                    .ForMember(r => r.Firstname, map => map.MapFrom(r => r.Vorname))
                    .ForMember(r => r.Lastname, map => map.MapFrom(r => r.Nachname))
                    .ForMember(r => r.YearOfBirth, map => map.MapFrom(r => r.Geburtsdatum.ToYear()))
                    .ForMember(r => r.City, map => map.MapFrom(r => r.Wohnort != "leer" ? r.Wohnort : null))
                    .ForMember(r => r.Email, map => map.MapFrom(r => r.Email != "leer" ? r.Email : null))
                    .ForMember(r => r.SportsClub, map => map.MapFrom(r => r.Verein != "leer" ? r.Verein : null))
                    .ForMember(r => r.CategoryId, map => map.MapFrom(r => categories.Single(category => category.Name == categoryMapping[r.Bewerb]).Id))
                    .ForMember(r => r.Id, map => map.Ignore())
                    .ForMember(r => r.RunningTime, map => map.Ignore())
                    .ForMember(r => r.Category, map => map.Ignore())
                    .ForMember(r => r.ChipId, map => map.Ignore())
                    .ForMember(r => r.RunningTime, map => map.Ignore())
                    .ForMember(r => r.TimeAtDestination, map => map.Ignore())
                    .ForMember(r => r.Startnumber, map => map.Ignore());
            });
        }
    }

    internal static partial class Extensions
    {
        internal static int ToYear(this string @this)
            => @this.Length == 4
            ? int.TryParse(@this, out var year)
                ? year : 0
            : DateTime.TryParse(@this, CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out var date)
                ? date.Year : 0;

        internal static void CheckCategory(this string @this, ImmutableDictionary<string, string> categories)
        {
            if (!categories.ContainsKey(@this))
                throw new InvalidDataException($"Category \"{@this}\" not defined.");
        }

        internal static string ReadToEndClean(this StreamReader @this)
            => @this
            .ReadToEnd()
            .Replace("&Auml;", "Ä")
            .Replace("&auml;", "ä")
            .Replace("&Euml;", "Ë")
            .Replace("&euml;", "ë")
            .Replace("&Iuml;", "Ï")
            .Replace("&iuml;", "ï")
            .Replace("&Ouml;", "Ö")
            .Replace("&ouml;", "ö")
            .Replace("&Uuml;", "Ü")
            .Replace("&uuml;", "ü")
            .Replace("&Yuml;", "Ÿ")
            .Replace("&yuml;", "ÿ")
            .Replace("&ndash;", "-");
    }
}