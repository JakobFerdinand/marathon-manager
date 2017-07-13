using Core.Models;
using Logic.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Data.Shared
{
    public class ChangesFinder : IChangesFinder
    {
        private readonly IDateTimeManager _dateTimeManager;

        public ChangesFinder(IDateTimeManager dateTimeManager)
        {
            _dateTimeManager = dateTimeManager;
        }

        public IEnumerable<ChangeLog> GetChanges(DbContext context)
        {
            var changeTime = _dateTimeManager.Now;

            var changes = new List<ChangeLog>();
            foreach (var entry in context.ChangeTracker.Entries())
            {

                var properties = entry.OriginalValues.Properties;
                var propertyNames = properties.Select(p => p.Name);

                var keyName = properties.First(p => p.IsPrimaryKey()).Name;
                var key = entry.Property(keyName);

                foreach (var propertyName in propertyNames)
                {
                    var property = entry.Property(propertyName);
                    if (property.IsModified)
                        changes.Add(new ChangeLog
                        {
                            EntityId = key.OriginalValue?.ToString(),
                            ChangeTime = changeTime,
                            TypeName = entry.Entity.GetType().Name,
                            PropertyName = propertyName,
                            OldValue = property.OriginalValue?.ToString(),
                            NewValue = property.CurrentValue?.ToString()
                        });
                }
            }
            return changes;
        }
    }
}
