using Core.Models;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class RunnerRepository : Repository<RunnerDbContext, Runner>, IRunnerRepository
    {
        public RunnerRepository(RunnerDbContext context)
            : base(context)
        { }

        public Runner GetIfHasNoTimeWithCategory(string chipId)
        {
            if (chipId is null)
                throw new ArgumentNullException(nameof(chipId));

            return Entries.AsNoTracking()
                .Include(r => r.Category)
                .SingleOrDefault(r => r.ChipId == chipId && r.TimeAtDestination == null);
        }

        public async Task<IEnumerable<Runner>> GetAllWithRelated(bool asNoTracking = false)
        {
            var query = Entries.Include(r => r.Category).AsQueryable();

            if (asNoTracking)
                query = query.AsNoTracking();

            return await query.ToListAsync();
        }   
    }
}