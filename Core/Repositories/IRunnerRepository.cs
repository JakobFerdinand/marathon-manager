using Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IRunnerRepository : IRepository<Runner>
    {
        Task<IEnumerable<Runner>> GetAllWithRelated(bool asNoTracking = false);
        Runner GetIfHasNoTimeWithCategory(string chipId);
    }
}