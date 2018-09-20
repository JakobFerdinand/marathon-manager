using AutoMapper;
using Core.Models;
using Core.Repositories;
using Data;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
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

        private static void Main(string[] args)
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
            var options = new DbContextOptionsBuilder<RunnerDbContext>()
                .UseSqlServer(Configuration.GetConnectionString("Default"))
                .Options;
            using (var context = new RunnerDbContext(options))
            {
                var categories = GenerateCategories2018();
                context.AddRange(categories);
                context.SaveChanges();

                WriteLine("Categories were successfully saved:");
                categories.ForEach(c => WriteLine($"| {c.Name}"));
            }

            ImmutableList<Category> GenerateCategories2018() => ImmutableList.Create(
                new Category { Name = "Hauptlauf, 10.000m", PlannedStartTime = new DateTime(2018, 09, 23, 10, 00, 00) },
                new Category { Name = "Hobbylauf, 3.800m", PlannedStartTime = new DateTime(2018, 09, 23, 10, 00, 00) },
                new Category { Name = "U16 Hobbylauf, 3.800m", PlannedStartTime = new DateTime(2018, 09, 23, 10, 00, 00) },
                new Category { Name = "AK 2007 und jünger, 500m", PlannedStartTime = new DateTime(2018, 09, 23, 09, 10, 00) },
                new Category { Name = "AK 2006 - 2003, 1.000m", PlannedStartTime = new DateTime(2018, 09, 23, 09, 20, 00) },
                new Category { Name = "Walken, 10.000m", PlannedStartTime = new DateTime(2018, 09, 23, 10, 00, 00) },
                new Category { Name = "Walken, 3.800m", PlannedStartTime = new DateTime(2018, 09, 23, 10, 00, 00) }
            );
        }

        private static void ImportRunners()
        {
            var path = string.Empty;
            while (!File.Exists(path))
                path = readPath();

            var filereader = new StreamReader(path);
            var json = filereader.ReadToEnd()
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

            var options = new DbContextOptionsBuilder<RunnerDbContext>()
                .UseSqlServer(Configuration.GetConnectionString("Default"))
                .Options;
            using (var context = new RunnerDbContext(options))
            {
                var categoryRepository = new CategoryRepository(context);
                var runnerRepository = new RunnerRepository(context);
                using (var unitOfWork = new UnitOfWork(context, categoryRepository, runnerRepository))
                {
                    var mapperConfiguration = GetMapperConfiguration(unitOfWork.Categories);
                    var mapper = new Mapper(mapperConfiguration);

                    var deserializer = new JsonDeserializer(mapper);

                    var runners = deserializer.Deserialize(json);

                    runnerRepository.AddRange(runners);
                    unitOfWork.Complete();

                    WriteLine($"{runners.Count()} Runners were successfully saved.");
                }
            }

            string readPath()
            {
                Write("Import File Path: ");
                return ReadLine();
            }
        }

        private static AutoMapper.IConfigurationProvider GetMapperConfiguration(ICategoryRepository categoryRepository)
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
                    .BeforeMap((i, _) => i.Spalte_19.CheckCategory(categories))
                    .ForMember(r => r.Gender, map => map.MapFrom(r => r.Spalte_17 == "Mann" ? Gender.Mann : Gender.Frau))
                    .ForMember(r => r.Firstname, map => map.MapFrom(r => r.Spalte_02))
                    .ForMember(r => r.Lastname, map => map.MapFrom(r => r.Spalte_03))
                    .ForMember(r => r.YearOfBirth, map => map.MapFrom(r => r.Spalte_05.ToYear()))
                    .ForMember(r => r.City, map => map.MapFrom(r => r.Spalte_06 != "leer" ? r.Spalte_06 : null))
                    .ForMember(r => r.Email, map => map.MapFrom(r => r.Spalte_08 != "leer" ? r.Spalte_08 : null))
                    .ForMember(r => r.SportsClub, map => map.MapFrom(r => r.Spalte_01 != "leer" ? r.Spalte_01 : null))
                    .ForMember(r => r.CategoryId, map => map.MapFrom(r => categories.Single(category => category.Name == r.Spalte_19).Id))
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
            : DateTime.TryParse(@this, out var date)
                ? date.Year : 0;

        internal static void CheckCategory(this string @this, ImmutableList<Category> categories)
        {
            if (!categories.Select(c => c.Name).Contains(@this))
                throw new Exception($"Category \"{@this}\" not in DB.");
        }
    }
}