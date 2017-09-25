using Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;

namespace Data.Tests.Configurations
{
    public class CategoryConfigurationTests
    {
        [Fact (Skip = "InMemory Databse does not validate the Entities saved to it.")]
        public void Add_Category_with_name_longer_than_50_chars_AnyRandomDatabaseExeption()
        {
            var options = new DbContextOptionsBuilder<RunnerDbContext>()
                .UseInMemoryDatabase("Add_Category_with_name_longer_than_50_chars_")
                .Options;
            using (var context = new RunnerDbContext(options))
            {
                var category = new Category
                {
                    Name =null, // "A Name longer than 50 Characters__40345678950Bllalbaldjflahsfduhalsdfj",
                };

                context.Categories.Add(category);
                Assert.ThrowsAny<Exception>(() => context.SaveChanges());
            }
        }
    }
}
