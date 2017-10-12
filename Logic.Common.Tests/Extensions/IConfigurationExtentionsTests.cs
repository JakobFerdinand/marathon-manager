using Microsoft.Extensions.Configuration;
using NSubstitute;
using Logic.Common.Extensions;
using Xunit;

namespace Logic.Common.Tests
{
    public class IConfigurationExtentionsTests
    {
        [Fact]
        [Trait("Unit", "")]
        public void GetLoggingPath_calles_GetSection_for_Logging_with_given_Name()
        {
            var configuration = Substitute.For<IConfiguration>();
            var configurationSection = Substitute.For<IConfigurationSection>();
            configuration.GetSection(null).ReturnsForAnyArgs(configurationSection);
            configurationSection["the path to log to"].Returns("worked");

            var result = configuration.GetLoggingPath("the path to log to");

            configuration.Received().GetSection("Logging");
            Assert.Equal("worked", result);
        }
    }
}
