using Core.Models;
using Core.Repositories;
using System.Collections.Immutable;
using System.Linq;

namespace Data.Repositories
{
    public class CategoryRepository : Repository<RunnerDbContext, Category>, ICategoryRepository
    {
        public CategoryRepository(RunnerDbContext context)
            : base(context)
        { }

        public ImmutableList<Category> GetNotStarted()
            => Entries.Where(c => c.Starttime == null).ToImmutableList();
    }
}