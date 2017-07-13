using System;

namespace Core.Models
{
    public class ChangeLog
    {
        public object Id { get; set; }
        public DateTime ChangeTime { get; set; }
        public string TypeName { get; set; }
        public string PropertyName { get; set; }
        public object OldValue { get; set; }
        public object NewValue { get; set; }
    }
}
