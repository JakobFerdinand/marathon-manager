using Core.Models;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using System;
using Xunit;

namespace Data.Tests.Repositories
{
    public class RepositoryTests
    {
        [Fact]
        public void Constructor_DbContext_Null_ArgumentNullException() => Assert.Throws<ArgumentNullException>(() => new EmptyRepositorySubClass(null));
        [Fact]
        public void CanCreateInstance()
        {
            var options = new DbContextOptionsBuilder<RunnerDbContext>().UseInMemoryDatabase(nameof(CanCreateInstance)).Options;
            var context = new RunnerDbContext(options);
            var repository = new EmptyRepositorySubClass(context);
            Assert.NotNull(repository);
        }

        internal class EmptyEntitySubClass : Entity { }
        internal class EmptyRepositorySubClass : Repository<RunnerDbContext, EmptyEntitySubClass>
        {
            public EmptyRepositorySubClass(RunnerDbContext context) : base(context)
            { }
        }
    }
}
