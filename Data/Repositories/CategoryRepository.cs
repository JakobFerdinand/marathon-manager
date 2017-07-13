using System;
using System.Collections.Generic;
using Core.Models;
using Core.Repositories;
using System.Linq;

namespace Data.Repositories
{
    public class CategoryRepository : Repository<RunnerDbContext, Category>, ICategoryRepository
    {
        public CategoryRepository(RunnerDbContext context)
            : base(context)
        { }

        public IEnumerable<Category> GetNotStarted()
        {
            return Entries.Where(c => c.Starttime == null).ToList();
        }
    }
}