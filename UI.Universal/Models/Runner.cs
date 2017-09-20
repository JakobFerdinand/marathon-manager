using System;

namespace UI.Universal.Models
{
    public class Runner
    {
        public int Startnumber { get; set; }
        public Gender Gender { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public TimeSpan RunningTime { get; set; }
        public string CategoryName { get; set; }
    }
}
