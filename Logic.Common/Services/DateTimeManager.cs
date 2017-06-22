using System;
using Logic.Common.Interfaces;

namespace Logic.Common.Services
{
    public class DateTimeManager : IDateTimeManager
    {
        public DateTime Now => DateTime.Now;
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
