using Core.Models;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Data.Repositories
{
    public class RunnerRepository : Repository<RunnersContext, Runner>, IRunnerRepository
    {
        public RunnerRepository(RunnersContext context)
            : base(context)
        { }

        public Runner GetIfHasNoTimeWithCategory(string chipId)
        {
            return Entries.Include(r => r.Category)
                          .SingleOrDefault(r => r.ChipId == chipId && r.TimeAtDestination == null);
        }
    }
}