using Core.Models;
using System.Collections.Generic;

namespace Core.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        IEnumerable<Category> GetNotStarted();
    }
}