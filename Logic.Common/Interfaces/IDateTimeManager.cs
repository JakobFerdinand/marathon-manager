using System;

namespace Logic.Common.Interfaces
{
    public interface IDateTimeManager
    {
        DateTime Now { get; }
        DateTime UtcNow { get; }
    }
}
