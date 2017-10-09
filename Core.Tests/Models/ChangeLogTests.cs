using Core.Models;
using System;
using Xunit;

namespace Core.Tests.Models
{
    public class ChangeLogTests
    {
        [Fact]
        public void CanCreateInstance()
        {
            var changeLog = new ChangeLog();
            Assert.NotNull(changeLog);
            Assert.Equal(0, changeLog.Id);
            Assert.Null(changeLog.EntityId);
            Assert.Equal(new DateTime(), changeLog.ChangeTime);
            Assert.Null(changeLog.TypeName);
            Assert.Null(changeLog.PropertyName);
            Assert.Null(changeLog.OldValue);
            Assert.Null(changeLog.NewValue);
        }
    }
}
