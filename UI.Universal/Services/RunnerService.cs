using Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UI.Universal.Interfaces;

namespace UI.Universal.Services
{
    internal class RunnerService : IRunnerService
    {
        public async Task<IEnumerable<Runner>> GetAll()
        {
            return new List<Runner>
            {
                new Runner { Id = 1, Startnumber = 1, Firstname = "Luis", Lastname = "Müller", RunningTime = new TimeSpan(00, 00, 48, 43, 333)},
                new Runner { Id = 2, Startnumber = 2, Firstname = "Nick", Lastname = "Müller", RunningTime = new TimeSpan(00, 00, 50, 52, 333)},
            };
        }
    }
}
