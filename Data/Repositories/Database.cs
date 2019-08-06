using Core.Repositories;

namespace Data.Repositories
{
    public class Database : IDatabase
    {
        private readonly RunnerDbContext context;

        public Database(RunnerDbContext context)
            => this.context = context ?? throw new System.ArgumentNullException(nameof(context));

        public bool CanConnect()
            => context.Database.CanConnect();
    }
}
