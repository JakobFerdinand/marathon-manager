using Core.Models;
using Xunit;

namespace Core.Tests.Models
{
    public class RunnerTests
    {
        [Fact]
        public void CanCreateInstance()
        {
            var runner = new Runner();
            Assert.NotNull(runner);
        }
    }
}
