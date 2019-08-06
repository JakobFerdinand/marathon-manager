using Core.Repositories;

namespace Data.Sample.Repositories
{
    public class Database : IDatabase
    {
        public bool CanConnect() => true;
    }
}
