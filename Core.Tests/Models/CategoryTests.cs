using Core.Models;
using System;
using Xunit;

namespace Core.Tests.Models
{
    public class CategoryTests
    {
        [Fact]
        [Trait("Unit", "")]
        public void CanCreateInstance()
        {
            var category = new Category();
            Assert.NotNull(category);
            Assert.NotNull(category.Runners);
            Assert.Null(category.Starttime);
            Assert.Equal(default(DateTime), category.PlannedStartTime);
            Assert.Null(category.Name);
        }
    }
}
