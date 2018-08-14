using Core.Models;
using System.Collections.Immutable;

namespace Core.Repositories
{
    public interface IRunnerRepository : IRepository<Runner>
    {
        ImmutableList<Runner> GetAllWithCategories(bool asNoTracking = true);
        Runner GetIfHasNoTimeWithCategory(string chipId);
    }
}