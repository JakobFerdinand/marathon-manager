using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class RunnerDbContext : DbContext
    {
        public RunnerDbContext(DbContextOptions<RunnerDbContext> options) 
            : base(options)
        { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Runner> Runners { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new Configurations.RunnerConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.CategoryConfiguration());
        }
    }
}