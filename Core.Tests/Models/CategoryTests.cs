using Core.Models;
using Xunit;

namespace MarathonManager.Core.Tests
{
    public class CategoryTests
    {
        [Fact]
        public void CanCreateInstance()
        {
            var category = new Category();
            Assert.NotNull(category);
        }
    }
}
