using System;

namespace Core.Models
{
    public class ChangeLog
    {
        public int Id { get; set; }
        public string EntityId { get; set; }
        public DateTime ChangeTime { get; set; }
        public string TypeName { get; set; }
        public string PropertyName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
    }
}
