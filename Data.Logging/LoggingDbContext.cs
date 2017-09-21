using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Logging
{
    public class LoggingDbContext : DbContext
    {
        public LoggingDbContext(DbContextOptions<LoggingDbContext> options) 
            : base(options)
        { }

        public DbSet<ChangeLog> ChangeLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) => modelBuilder.ApplyConfiguration(new Configurations.ChangeLogConfiguration());
    }
}
