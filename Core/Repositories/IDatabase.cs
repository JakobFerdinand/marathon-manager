using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IDatabase
    {
        Task<bool> CanConnectAsync();
    }
}
