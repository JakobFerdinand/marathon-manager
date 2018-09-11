using Core.Models;
using Data;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Tynamix.ObjectFiller;

namespace SampleData
{
    class Program
    {
        static void Main(string[] args)
        {
            EnsureNewDatabase();
            SaveCategories2017();
        }

        private static void EnsureNewDatabase(string connectionString = null)
        {
            var options = new DbContextOptionsBuilder<RunnerDbContext>()
                .UseSqlServer(connectionString ?? "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MarathonManager;Integrated Security=True")
                .Options;

            using (var context = new RunnerDbContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }
        }

        private static void CalculateTimes()
        {
            var optionsBuilder = new DbContextOptionsBuilder<RunnerDbContext>();
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=MarathonManager;Integrated Security=True");

            using (var context = new RunnerDbContext(optionsBuilder.Options))
            using (var unitOfWork = new UnitOfWork(context, new CategoryRepository(context), new RunnerRepository(context), new EmptyChangesFinder(), new EmptyChangesLogger()))
            {
                var r = context.Runners.SingleOrDefault(rx => rx.Startnumber == 193);
                var category = context.Categories.Single(c => c.Id == 14);
                    r.RunningTime = r.TimeAtDestination - category.Starttime;
                    Console.WriteLine($"{r.Startnumber, 3} | {r.TimeAtDestination} | {r.RunningTime}");

                context.SaveChanges();
            }

            Console.ReadKey();
        }

        private static void GenerateSampleRunningTimes()
        {
            var optionsBuilder = new DbContextOptionsBuilder<RunnerDbContext>();
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MarathonManager;Integrated Security=True");

            using (var context = new RunnerDbContext(optionsBuilder.Options))
            using (var unitOfWork = new UnitOfWork(context, new CategoryRepository(context), new RunnerRepository(context), new EmptyChangesFinder(), new EmptyChangesLogger()))
            {
                var runners = unitOfWork.Runners.GetAll().ToList();

                var random = new Random();
                for (int i = 0; i < runners.Count; i++)
                {
                    runners[i].Startnumber = i + 1;
                    runners[i].RunningTime = new TimeSpan(0, random.Next(15, 59), random.Next(0, 59));
                }

                unitOfWork.Complete();
            }
        }


        private static void GenerateSampleData()
        {
            Console.WriteLine("Generate Sample Data.");
            var runners = GenerateRunners();
            var categories = GenerateCategories2017();
            SaveData(runners, categories);
            Console.WriteLine("Generate Sample Data Complete.");
            Console.ReadKey();
        }

        private static void SaveData(IEnumerable<Runner> runners, IEnumerable<Category> categories)
        {
            var optionsBuilder = new DbContextOptionsBuilder<RunnerDbContext>();
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MarathonManager;Integrated Security=True");

            using (var context = new RunnerDbContext(optionsBuilder.Options))
            using (var unitOfWork = new UnitOfWork(context, new CategoryRepository(context), new RunnerRepository(context), new EmptyChangesFinder(), new EmptyChangesLogger()))
            {
                unitOfWork.Categories.AddRange(categories);
                unitOfWork.Complete();

                var categoriesFromDb = unitOfWork.Categories.GetAll().ToList();

                var random = new Random();
                foreach (var runner in runners)
                {
                    runner.Category = categoriesFromDb[random.Next(0, categoriesFromDb.Count)];
                }

                unitOfWork.Runners.AddRange(runners);
                unitOfWork.Complete();
            }
        }

        private static void SaveCategories2017(string connectionString = null)
        {
            var options = new DbContextOptionsBuilder<RunnerDbContext>()
                .UseSqlServer(connectionString ?? "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MarathonManager;Integrated Security=True")
                .Options;

            using (var context = new RunnerDbContext(options))
            {
                context.AddRange(GenerateCategories2017());
                context.SaveChanges();
            }
        }
        private static IEnumerable<Category> GenerateCategories2017() => new List<Category>
        {
            new Category { Name = "Hauptlauf, 10.000m", PlannedStartTime = DateTime.Now },
            new Category { Name = "Hobbylauf, 3.800m", PlannedStartTime = DateTime.Now },
            new Category { Name = "AK 2005 und j&uuml;nger, 500m", PlannedStartTime = DateTime.Now },
            new Category { Name = "AK 2004 &ndash; 2001, 1.000m", PlannedStartTime = DateTime.Now },
            new Category { Name = "Walken, 10.000m", PlannedStartTime = DateTime.Now },
            new Category { Name = "Walken, 3.800m", PlannedStartTime = DateTime.Now }
        };

        private static IEnumerable<Runner> GenerateRunners()
        {
            var runnerFiller = new Filler<Runner>();
            runnerFiller.Setup()
                .OnProperty(r => r.Id).IgnoreIt()
                .OnProperty(r => r.Category).IgnoreIt()
                .OnProperty(r => r.CategoryId).IgnoreIt()
                .OnProperty(r => r.ChipId).IgnoreIt()
                .OnProperty(r => r.RunningTime).IgnoreIt()
                .OnProperty(r => r.TimeAtDestination).IgnoreIt()
                .OnProperty(r => r.Startnumber).Use(new IntRange(1, 500))
                .OnProperty(r => r.YearOfBirth).Use(new IntRange(DateTime.Now.Year - 70, DateTime.Now.Year - 3))
                .OnProperty(r => r.Firstname).Use(new RealNames(NameStyle.FirstName))
                .OnProperty(r => r.Lastname).Use(new RealNames(NameStyle.LastName))
                .OnProperty(r => r.SportsClub).Use(new RandomListItem<string>(
                    "SC Mining", null, "SV Ried", "SV Altheim"));

            return runnerFiller.Create(200);
        }
    }
}