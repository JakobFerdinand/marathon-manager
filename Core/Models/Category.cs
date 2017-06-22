using System;
using System.Collections.Generic;

namespace Core.Models
{
    public class Category : Entity
    {
        public string Name { get; set; }
        public DateTime? Starttime { get; set; }
        public DateTime PlannedStartTime { get; set; }

        public ICollection<Runner> Runners { get; } = new HashSet<Runner>();
    }
}