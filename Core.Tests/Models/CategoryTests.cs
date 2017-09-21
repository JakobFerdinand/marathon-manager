using Core.Models;
using Xunit;

namespace Core.Tests.Models
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
