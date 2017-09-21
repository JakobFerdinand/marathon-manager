using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    internal class RunnerConfiguration : IEntityTypeConfiguration<Runner>
    {
        public void Configure(EntityTypeBuilder<Runner> builder)
        {
            builder.ToTable("Runners");
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id)
                .HasColumnName("RunnerId");
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
            builder.Property(c => c.SportsClub)
                .HasColumnName("SportsClub")
                .IsRequired(false)
                .HasMaxLength(200)
                .IsUnicode();
            builder.Property(r => r.City)
                .HasMaxLength(50)
                .IsUnicode();
            builder.Property(r => r.Email)
                .HasMaxLength(50)
                .IsUnicode();

            builder.HasOne(r => r.Category)
                .WithMany(c => c.Runners)
                .HasForeignKey(r => r.CategoryId)
                .HasConstraintName("FK_Category_Runners");
        }
    }
}
