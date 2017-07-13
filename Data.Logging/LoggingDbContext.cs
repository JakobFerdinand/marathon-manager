using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Logging
{
    public class LoggingDbContext : DbContext
    {
        public LoggingDbContext(DbContextOptions<LoggingDbContext> options) : base(options)
        { }

        public DbSet<ChangeLog> ChangeLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChangeLog>(c =>
            {
                c.ForSqlServerToTable("ChangeLogs");
                c.HasKey(cl => cl.Id);
                c.Property(cl => cl.Id)
                    .ForSqlServerHasColumnName("ChangeLogId");
                c.Property(cl => cl.EntityId)
                    .HasMaxLength(10)
                    .IsUnicode(false);
                c.Property(cl => cl.ChangeTime)
                    .ForSqlServerHasColumnType("datetime2");
                c.Property(cl => cl.TypeName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                c.Property(cl => cl.PropertyName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }
    }
}
