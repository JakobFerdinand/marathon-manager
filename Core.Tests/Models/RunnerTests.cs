using Core.Models;
using Xunit;

namespace Core.Tests.Models
{
    public class RunnerTests
    {
        [Fact]
        [Trait("Unit", "")]
        public void CanCreateInstance()
        {
            var runner = new Runner();
            Assert.NotNull(runner);
            Assert.Null(runner.Startnumber);
            Assert.Equal(default(Gender), runner.Gender);
            Assert.Null(runner.Firstname);
            Assert.Null(runner.Lastname);
            Assert.Null(runner.SportsClub);
            Assert.Null(runner.City);
            Assert.Null(runner.Email);
            Assert.Equal(default(int), runner.YearOfBirth);
            Assert.Null(runner.ChipId);
            Assert.Null(runner.TimeAtDestination);
            Assert.Null(runner.RunningTime);
            Assert.Equal(default(int), runner.CategoryId);
            Assert.Null(runner.Category);
        }
    }
}
