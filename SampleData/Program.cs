﻿using Core.Models;
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
            //GenerateSampleRunningTimes();
        }

        private static void GenerateSampleRunningTimes()
        {
            var optionsBuilder = new DbContextOptionsBuilder<RunnerDbContext>();
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=MarathonManager;Integrated Security=True");

            using (var context = new RunnerDbContext(optionsBuilder.Options))
            using (var unitOfWork = new UnitOfWork(context, new CategoryRepository(context), new RunnerRepository(context), new EmptyChangesFinder(), new EmptyChangesLogger()))
            {
                var runners = unitOfWork.Runners.GetAll().ToList();

                var random = new Random();
                for (int i = 0; i < runners.Count; i++)
                {
                    runners[i].Startnumber = i+1;
                    runners[i].RunningTime = new TimeSpan(0, random.Next(15, 59), random.Next(0, 59));
                }

                unitOfWork.Complete();
            }
        }


        private static void GenerateSampleData()
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
            var optionsBuilder = new DbContextOptionsBuilder<RunnerDbContext>();
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=MarathonManager;Integrated Security=True");

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

        private static IEnumerable<Category> GenerateCategories() => new List<Category>
        {
            new Category { Name = "Walken 3000", PlannedStartTime = DateTime.Now },
            new Category { Name = "Walken 10000", PlannedStartTime = DateTime.Now },
            new Category { Name = "Laufen 3000", PlannedStartTime = DateTime.Now },
            new Category { Name = "Laufen 10000", PlannedStartTime = DateTime.Now }
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