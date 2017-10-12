using UI.RunnerManagement.Validation;
using Xunit;

namespace UI.RunnerManagement.Tests.Validation
{
    public class IsRequiredValidationRuleTests
    {
        [Fact]
        public void CanCreateInstance()
        {
            var rule = new IsRequiredValidationRule();
            Assert.NotNull(rule);
            Assert.Null(rule.Fieldname);
        }
    }
}
