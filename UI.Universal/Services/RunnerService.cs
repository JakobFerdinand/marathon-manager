using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using UI.Universal.Interfaces;
using UI.Universal.Models;

namespace UI.Universal.Services
{
    internal class RunnerService : IRunnerService
    {
        private const string baseUrl = "http://localhost:5000/api/runners";

        public async Task<IEnumerable<Runner>> GetAll()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(baseUrl);
                var json = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<IEnumerable<Runner>>(json);
            }
        }
    }
}
