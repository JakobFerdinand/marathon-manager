using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;

namespace Data.Tests.Repositories
{
    public class RunnerRepositoryTests
    {
        [Fact]
        [Trait("Unit", "")]
        public void Constructor_DbContext_Null_should_thorw_ArgumentNullException() => Assert.Throws<ArgumentNullException>(() => new RunnerRepository(null));
        [Fact]
        [Trait("Unit", "")]
        public void GetIfHasNoTimeWithCategory_should_throw_ArugmentNullException_when_given_chipId_is_null()
        {
            var options = new DbContextOptionsBuilder<RunnerDbContext>()
                .UseInMemoryDatabase(nameof(GetIfHasNoTimeWithCategory_should_throw_ArugmentNullException_when_given_chipId_is_null))
                .Options;
            var context = new RunnerDbContext(options);
            var repository = new RunnerRepository(context);

            Assert.Throws<ArgumentNullException>("chipId", () => repository.GetIfHasNoTimeWithCategory(null));
        }
    }
}
