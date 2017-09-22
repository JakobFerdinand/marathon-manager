using Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Data.Tests
{
    public class RunnerDbContextTests
    {
        [Fact]
        public void Constructor_DbContextOptions_null_ArgumentNullException() => Assert.Throws<ArgumentNullException>(() => new RunnerDbContext(null));
        [Fact]
        public void CanCreateInstance()
        {
            var options = new DbContextOptionsBuilder<RunnerDbContext>().Options;
            using (var context = new RunnerDbContext(options))
            {
                Assert.NotNull(context);
                Assert.NotNull(context.Categories);
                Assert.NotNull(context.Runners);
            }
        }
        [Fact]
        public void CRUD_Operations_for_Categories()
        {
            var options = new DbContextOptionsBuilder<RunnerDbContext>()
                .UseInMemoryDatabase("CRUD_Operations_for_Categories")
                .Options;
            using (var context = new RunnerDbContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var categories = new[]
                {
                    new Category { Name = "Category1", PlannedStartTime = new DateTime(2017, 09, 22, 18, 05, 00)},
                    new Category { Name = "Category2", PlannedStartTime = new DateTime(2000, 01, 01, 10, 55, 00)},
                    new Category { Name = "Category3", PlannedStartTime = new DateTime(2012, 12, 31, 00, 00, 00)},
                };

                context.Categories.AddRange(categories);
                context.SaveChanges();
            }

            using (var context = new RunnerDbContext(options))
            {
                var categories = context.Categories.ToList();

                Assert.Equal(3, categories.Count);
                Assert.Equal(18, categories[0].PlannedStartTime.Hour);
                Assert.Equal("Category2", categories[1].Name);
                Assert.Equal(0, categories[2].PlannedStartTime.Millisecond);
                Assert.Null(categories[1].Starttime);

                categories[0].Name = "New Category Name Number 1";
                categories[1].PlannedStartTime = new DateTime(2004, 02, 29, 13, 14, 00);
                categories[2].Name = "Whatever";

                context.SaveChanges();
            }

            using (var context = new RunnerDbContext(options))
            {
                var categories = context.Categories.ToList();

                Assert.Equal(3, categories.Count);
                Assert.Equal("New Category Name Number 1", categories[0].Name);
                Assert.Equal(2004, categories[1].PlannedStartTime.Year);
                Assert.Equal("Whatever", categories[2].Name);

                context.Remove(categories[1]);
                context.SaveChanges();
            }

            using (var context = new RunnerDbContext(options))
            {
                var categories = context.Categories.ToList();

                Assert.Equal(2, categories.Count);

                context.Categories.RemoveRange(categories);
                context.SaveChanges();
            }

            using (var context = new RunnerDbContext(options))
            {
                var countCategories = context.Categories.Count();

                Assert.Equal(0, countCategories);
            }
        }
        [Fact]
        public async Task CRUD_Operations_for_Categories_Async()
        {
            var options = new DbContextOptionsBuilder<RunnerDbContext>()
                .UseInMemoryDatabase("CRUD_Operations_for_Categories_Async")
                .Options;
            using (var context = new RunnerDbContext(options))
            {
                await context.Database.EnsureDeletedAsync();
                await context.Database.EnsureCreatedAsync();

                var categories = new[]
                {
                    new Category { Name = "Category1", PlannedStartTime = new DateTime(2017, 09, 22, 18, 05, 00)},
                    new Category { Name = "Category2", PlannedStartTime = new DateTime(2000, 01, 01, 10, 55, 00)},
                    new Category { Name = "Category3", PlannedStartTime = new DateTime(2012, 12, 31, 00, 00, 00)},
                };

                await context.Categories.AddRangeAsync(categories);
                await context.SaveChangesAsync();
            }

            using (var context = new RunnerDbContext(options))
            {
                var categories = await context.Categories.ToListAsync();

                Assert.Equal(3, categories.Count);
                Assert.Equal(18, categories[0].PlannedStartTime.Hour);
                Assert.Equal("Category2", categories[1].Name);
                Assert.Equal(0, categories[2].PlannedStartTime.Millisecond);
                Assert.Null(categories[1].Starttime);

                categories[0].Name = "New Category Name Number 1";
                categories[1].PlannedStartTime = new DateTime(2004, 02, 29, 13, 14, 00);
                categories[2].Name = "Whatever";

                await context.SaveChangesAsync();
            }

            using (var context = new RunnerDbContext(options))
            {
                var categories = await context.Categories.ToListAsync();

                Assert.Equal(3, categories.Count);
                Assert.Equal("New Category Name Number 1", categories[0].Name);
                Assert.Equal(2004, categories[1].PlannedStartTime.Year);
                Assert.Equal("Whatever", categories[2].Name);

                context.Remove(categories[1]);
                await context.SaveChangesAsync();
            }

            using (var context = new RunnerDbContext(options))
            {
                var categories = await context.Categories.ToListAsync();

                Assert.Equal(2, categories.Count);

                context.Categories.RemoveRange(categories);
                await context.SaveChangesAsync();
            }

            using (var context = new RunnerDbContext(options))
            {
                var countCategories = await context.Categories.CountAsync();

                Assert.Equal(0, countCategories);
            }
        }
    }
}
