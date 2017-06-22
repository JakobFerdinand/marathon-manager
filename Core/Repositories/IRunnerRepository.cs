using Core.Models;

namespace Core.Repositories
{
    public interface IRunnerRepository : IRepository<Runner>
    {
        Runner GetIfHasNoTimeWithCategory(string chipId);
    }
}