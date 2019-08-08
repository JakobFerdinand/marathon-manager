using Core.Repositories;
using System;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class Database : IDatabase
    {
        private readonly RunnerDbContext context;

        public Database(RunnerDbContext context)
            => this.context = context ?? throw new System.ArgumentNullException(nameof(context));

        public async Task<bool> CanConnectAsync()
        {
            try
            {
                return await context.Database.CanConnectAsync();
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
