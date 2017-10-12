using Core.Models;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Data.Tests.Integration
{
    public class RunnerRepositoryTests
    {
        [Fact]
        [Trait("Integration", "Sql Server")]
        public async Task GetIfHasNoTimeWithCategory_should_return_null_if_no_runner_with_given_chipId_is_found()
        {
            var databaseName = nameof(GetIfHasNoTimeWithCategory_should_return_null_if_no_runner_with_given_chipId_is_found);
            var options = new DbContextOptionsBuilder<RunnerDbContext>()
                .UseSqlServer(string.Format(TestConfiguration.ConnectionString, databaseName))
                .Options;
            using (var context = new RunnerDbContext(options))
            {
                var repository = new RunnerRepository(context);

                await context.Database.EnsureDeletedAsync();
                await context.Database.EnsureCreatedAsync();

                var result = repository.GetIfHasNoTimeWithCategory("0123456789");

                await context.Database.EnsureDeletedAsync();

                Assert.Null(result);
            }
        }
        [Fact]
        [Trait("Integration", "Sql Server")]
        public async Task GetIfHasNoTimeWithCategory_should_return_runner_with_category_with_given_chipId()
        {
            var databaseName = nameof(GetIfHasNoTimeWithCategory_should_return_runner_with_category_with_given_chipId);
            var options = new DbContextOptionsBuilder<RunnerDbContext>()
                .UseSqlServer(string.Format(TestConfiguration.ConnectionString, databaseName))
                .Options;
            using (var context = new RunnerDbContext(options))
            {
                await context.Database.EnsureDeletedAsync();
                await context.Database.EnsureCreatedAsync();

                await context.Runners.AddAsync(new Runner
                {
                    ChipId = "4329085723",
                    Firstname = "Firstname",
                    Lastname = "Lastname",
                    Gender = Gender.Mann,
                    YearOfBirth = 2000,
                    TimeAtDestination = null,
                    Category = new Category
                    {
                        Name = "The Name of the Category",
                        PlannedStartTime = new DateTime(2017, 10, 12, 09, 00, 00)
                    }
                });
                await context.SaveChangesAsync();
            }

            using (var context = new RunnerDbContext(options))
            {
                var repository = new RunnerRepository(context);
                var result = repository.GetIfHasNoTimeWithCategory("4329085723");

                Assert.NotNull(result);
                Assert.NotNull(result.Category);
                Assert.Equal("Firstname", result.Firstname);
                Assert.Equal("Lastname", result.Lastname);
                Assert.Equal(Gender.Mann, result.Gender);
                Assert.Equal(Gender.Mann, result.Gender);
                Assert.Equal("The Name of the Category", result.Category.Name);
                Assert.Equal(new DateTime(2017, 10, 12, 09, 00, 00), result.Category.PlannedStartTime);
            }

            using (var context = new RunnerDbContext(options))
                await context.Database.EnsureDeletedAsync();
        }
        [Fact]
        [Trait("Integration", "Sql Server")]
        public async Task GetIfHasNoTimeWithCategory_should_reload_runner_and_category_with_given_chipId_if_aked_more_often()
        {
            var databaseName = nameof(GetIfHasNoTimeWithCategory_should_reload_runner_and_category_with_given_chipId_if_aked_more_often);
            var options = new DbContextOptionsBuilder<RunnerDbContext>()
                .UseSqlServer(string.Format(TestConfiguration.ConnectionString, databaseName))
                .Options;
            using (var context = new RunnerDbContext(options))
            {
                await context.Database.EnsureDeletedAsync();
                await context.Database.EnsureCreatedAsync();

                await context.Runners.AddAsync(new Runner
                {
                    ChipId = "4329085723",
                    Firstname = "Firstname",
                    Lastname = "Lastname",
                    Gender = Gender.Mann,
                    YearOfBirth = 2000,
                    TimeAtDestination = null,
                    Category = new Category
                    {
                        Name = "The Name of the Category",
                        PlannedStartTime = new DateTime(2017, 10, 12, 09, 00, 00)
                    }
                });
                await context.SaveChangesAsync();
            }

            using (var context = new RunnerDbContext(options))
            {
                var repository = new RunnerRepository(context);
                var result1 = repository.GetIfHasNoTimeWithCategory("4329085723");
                var result2 = repository.GetIfHasNoTimeWithCategory("4329085723");
                var result3 = repository.GetIfHasNoTimeWithCategory("4329085723");

                Assert.NotSame(result1, result2);
                Assert.NotSame(result1, result3);
                Assert.NotSame(result2, result3);
                Assert.NotSame(result1.Category, result2.Category);
                Assert.NotSame(result1.Category, result3.Category);
                Assert.NotSame(result2.Category, result3.Category);

                Assert.Equal(result1.Id, result2.Id);
                Assert.Equal(result1.Id, result3.Id);
                Assert.Equal(result2.Id, result3.Id);
                Assert.Equal(result1.Category.Id, result2.Category.Id);
                Assert.Equal(result1.Category.Id, result3.Category.Id);
                Assert.Equal(result2.Category.Id, result3.Category.Id);
            }

            using (var context = new RunnerDbContext(options))
                await context.Database.EnsureDeletedAsync();
        }
        [Fact]
        [Trait("Integration", "Sql Server")]
        public async Task GetIfHasNoTimeWithCategory_should_return_null_for_given_chipId_because_runners_timeAtDestiantion_is_not_null()
        {
            var databaseName = nameof(GetIfHasNoTimeWithCategory_should_return_null_for_given_chipId_because_runners_timeAtDestiantion_is_not_null);
            var options = new DbContextOptionsBuilder<RunnerDbContext>()
                .UseSqlServer(string.Format(TestConfiguration.ConnectionString, databaseName))
                .Options;
            using (var context = new RunnerDbContext(options))
            {
                await context.Database.EnsureDeletedAsync();
                await context.Database.EnsureCreatedAsync();

                await context.Runners.AddAsync(new Runner
                {
                    ChipId = "4329085723",
                    Firstname = "Firstname",
                    Lastname = "Lastname",
                    Gender = Gender.Mann,
                    YearOfBirth = 2000,
                    TimeAtDestination = new DateTime(2017, 10, 12, 12, 00, 00),
                    Category = new Category
                    {
                        Name = "The Name of the Category",
                        PlannedStartTime = new DateTime(2017, 10, 12, 09, 00, 00)
                    }
                });
                await context.SaveChangesAsync();
            }

            using (var context = new RunnerDbContext(options))
            {
                var repository = new RunnerRepository(context);
                var result = repository.GetIfHasNoTimeWithCategory("4329085723");

                Assert.Null(result);
            }

            using (var context = new RunnerDbContext(options))
                await context.Database.EnsureDeletedAsync();
        }
    }
}
