using Core.Models;
using Core.Repositories;

namespace Data.Repositories
{
    public class CategoryRepository : Repository<RunnersContext, Category>, ICategoryRepository
    {
        public CategoryRepository(RunnersContext context)
            : base(context)
        { }
    }
}