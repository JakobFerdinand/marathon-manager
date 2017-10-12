using Core.Models;
using Xunit;

namespace Core.Tests.Models
{
    public class EntityTests
    {
        [Fact]
        public void NewEntity_Id_property_should_be_default()
        {
            var entity = new EmptyEntitySubClass();
            Assert.Equal(default(int), entity.Id);
        }

        private class EmptyEntitySubClass : Entity { }
    }
}
