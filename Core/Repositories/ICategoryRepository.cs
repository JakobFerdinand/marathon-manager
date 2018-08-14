using Core.Models;
using System.Collections.Immutable;

namespace Core.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        ImmutableList<Category> GetNotStarted();
    }
}