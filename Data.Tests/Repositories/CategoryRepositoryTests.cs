using Data.Repositories;
using System;
using Xunit;

namespace Data.Tests.Repositories
{
    public class CategoryRepositoryTests
    {
        [Fact]
        public void Constructor_DbContext_Null_ArgumentNullException() => Assert.Throws<ArgumentNullException>(() => new CategoryRepository(null));
    }
}
