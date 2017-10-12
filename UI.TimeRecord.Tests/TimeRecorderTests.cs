using Core;
using Logging.Interfaces;
using Logic.Common.Interfaces;
using NSubstitute;
using System;
using Xunit;

namespace UI.TimeRecord.Tests
{
    public class TimeRecorderTests
    {
        [Fact]
        [Trait("Unit", "")]
        public void Constructor_all_parameter_null_ArgumentNullException() => Assert.Throws<ArgumentNullException>(() => new TimeRecorder(null, null, null, null));
        [Fact]
        [Trait("Unit", "")]
        public void Constructor_dateTimeManager_null_ArgumentNullException()
        {
            var logger = Substitute.For<ILogger>();
            var reader = Substitute.For<IReader>();
            var unitOfWork = Substitute.For<IUnitOfWork>();
            Assert.Throws<ArgumentNullException>(() => new TimeRecorder(null, logger, reader, unitOfWork));
        }
        [Fact]
        [Trait("Unit", "")]
        public void Constructor_logger_null_ArgumentNullException()
        {
            var dateTimeManager = Substitute.For<IDateTimeManager>();
            var reader = Substitute.For<IReader>();
            var unitOfWork = Substitute.For<IUnitOfWork>();
            Assert.Throws<ArgumentNullException>(() => new TimeRecorder(dateTimeManager, null, reader, unitOfWork));
        }
        [Fact]
        [Trait("Unit", "")]
        public void Constructor_reader_null_ArgumentNullException()
        {
            var dateTimeManager = Substitute.For<IDateTimeManager>();
            var logger = Substitute.For<ILogger>();
            var unitOfWork = Substitute.For<IUnitOfWork>();
            Assert.Throws<ArgumentNullException>(() => new TimeRecorder(dateTimeManager, logger, null, unitOfWork));
        }
        [Fact]
        [Trait("Unit", "")]
        public void Constructor_unitOfWork_null_ArgumentNullException()
        {
            var dateTimeManager = Substitute.For<IDateTimeManager>();
            var logger = Substitute.For<ILogger>();
            var reader = Substitute.For<IReader>();
            Assert.Throws<ArgumentNullException>(() => new TimeRecorder(dateTimeManager, logger, reader, null));
        }
        [Fact]
        [Trait("Unit", "")]
        public void CanCreateInstance()
        {
            var dateTimeManager = Substitute.For<IDateTimeManager>();
            var logger = Substitute.For<ILogger>();
            var reader = Substitute.For<IReader>();
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var recorder = new TimeRecorder(dateTimeManager, logger, reader, unitOfWork);
        }
    }
}
