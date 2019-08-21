using Core.Repositories;
using System.Collections.Immutable;
using System.Threading.Tasks;

namespace Data.Sample.Repositories
{
    public class Database : IDatabase
    {
        public Task<bool> CanConnectAsync() => Task.FromResult(true);
        public ImmutableArray<string> GetAvailableServers() => ImmutableArray.Create("Server 1", "Server 2", "Server 3");
    }
}
