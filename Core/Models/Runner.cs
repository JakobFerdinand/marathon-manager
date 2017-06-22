using System;

namespace Core.Models
{
    public class Runner : Entity
    {
        public int? Startnumber { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int YearOfBirth { get; set; }
        public string ChipId { get; set; }

        public DateTime? TimeAtDestination { get; set; }
        public TimeSpan? RunningTime { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}