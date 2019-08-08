using Core.Repositories;
using System.Threading.Tasks;

namespace Data.Sample.Repositories
{
    public class Database : IDatabase
    {
        public Task<bool> CanConnectAsync() => Task.FromResult(true);
    }
}
