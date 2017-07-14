using System;

namespace Web.Controllers.Resources
{
    public class RunnerResource
    {
        public int Startnumber { get; set; }
        public GenderResource Gender { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public TimeSpan RunningTime { get; set; }
        public string CategoryName { get; set; }
    }
}
