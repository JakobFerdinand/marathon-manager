using Core.Models;
using System.Collections.Immutable;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IRunnerRepository : IRepository<Runner>
    {
        Task<ImmutableList<Runner>> GetAllWithRelated(bool asNoTracking = false);
        Runner GetIfHasNoTimeWithCategory(string chipId);
    }
}