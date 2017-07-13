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
            Console.WriteLine("Generate Sample Data.");
            var runners = GenerateRunners();
            var categories = GenerateCategories();
            SaveData(runners, categories);
            Console.WriteLine("Generate Sample Data Complete.");
            Console.ReadKey();
        }

        private static void SaveData(IEnumerable<Runner> runners, IEnumerable<Category> categories)
        {
            var optionsBuilder = new DbContextOptionsBuilder<RunnersContext>();
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=MarathonManager;Integrated Security=True");

            using (var context = new RunnersContext(optionsBuilder.Options))
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

        private static IEnumerable<Category> GenerateCategories()
        {
            return new List<Category>
            {
                new Category { Name = "Walken 3000", PlannedStartTime = DateTime.Now },
                new Category { Name = "Walken 10000", PlannedStartTime = DateTime.Now },
                new Category { Name = "Laufen 3000", PlannedStartTime = DateTime.Now },
                new Category { Name = "Laufen 10000", PlannedStartTime = DateTime.Now }
            };
        }

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