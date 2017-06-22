using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data
{
    public class RunnersContext : DbContext
    {
        public RunnersContext(DbContextOptions<RunnersContext> options) 
            : base(options)
        { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Runner> Runners { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>(ConfigureCategory);
            modelBuilder.Entity<Runner>(RunnerConfiguration);
        }

        private void RunnerConfiguration(EntityTypeBuilder<Runner> builder)
        {
            builder.ForSqlServerToTable("Runners");
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id).ForSqlServerHasColumnName("RunnerId");
            builder.Property(c => c.Firstname)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(c => c.Lastname)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(c => c.ChipId)
                .IsRequired(false)
                .HasMaxLength(10)
                .IsUnicode(false);

            builder.HasOne(r => r.Category)
                .WithMany(c => c.Runners)
                .HasForeignKey(r => r.CategoryId)
                .HasConstraintName("FK_Category_Runners");
        }

        private void ConfigureCategory(EntityTypeBuilder<Category> builder)
        {
            builder.ForSqlServerToTable("Categories");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ForSqlServerHasColumnName("CategoryId");
            builder.Property(c => c.Name)
                .ForSqlServerHasColumnName("Name")
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}