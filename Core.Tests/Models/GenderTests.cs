using Core.Models;
using Xunit;

namespace Core.Tests.Models
{
    public class GenderTests
    {
        [Fact]
        public void Mann_should_have_value_1() => Assert.Equal(1, (int)Gender.Mann);
        [Fact]
        public void Frau_should_have_value_2() => Assert.Equal(2, (int)Gender.Frau);
    }
}
