using System.Collections.Immutable;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IDatabase
    {
        Task<bool> CanConnectAsync();
        bool IsServerOnline(string server);
        ImmutableArray<string> GetAvailableServers();
        ImmutableArray<string> GetAllDatabases(string server);
        bool EnsureDeleted();
        bool EnsureCreated();
    }
}
