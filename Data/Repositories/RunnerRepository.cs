using Core.Models;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Immutable;
using System.Linq;

namespace Data.Repositories
{
    public class RunnerRepository : Repository<RunnerDbContext, Runner>, IRunnerRepository
    {
        public RunnerRepository(RunnerDbContext context)
            : base(context)
        { }

        Runner GetWithCategoryById(int id)
            => Entries
            .Include(r => r.Category)
            .FirstOrDefault(r => r.Id == id);

        public Runner GetIfHasNoTimeWithCategory(string chipId)
        {
            if (chipId is null)
                throw new ArgumentNullException(nameof(chipId));

            return Entries.AsNoTracking()
                .Include(r => r.Category)
                .SingleOrDefault(r => r.ChipId == chipId && r.TimeAtDestination == null);
        }

        public ImmutableList<Runner> GetAllWithCategories(bool asNoTracking = true)
        {
            var query = Entries.Include(r => r.Category)
                .OrderBy(r => r.Startnumber)
                .AsQueryable();

            if (asNoTracking)
                query = query.AsNoTracking();

            return query.ToImmutableList();
        }

        Runner IRunnerRepository.GetWithCategoryById(int id)
            => Entries.Include(r => r.Category).FirstOrDefault(r => r.Id == id);
    }
}