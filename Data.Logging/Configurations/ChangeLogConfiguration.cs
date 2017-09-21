using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Logging.Configurations
{
    internal class ChangeLogConfiguration : IEntityTypeConfiguration<ChangeLog>
    {
        public void Configure(EntityTypeBuilder<ChangeLog> builder)
        {
            builder.ToTable("ChangeLogs");

            builder.HasKey(cl => cl.Id);

            builder.Property(cl => cl.Id)
                .HasColumnName("ChangeLogId");

            builder.Property(cl => cl.EntityId)
                .HasMaxLength(10)
                .IsUnicode(false);

            builder.Property(cl => cl.ChangeTime)
                .HasColumnType("datetime2");

            builder.Property(cl => cl.TypeName)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(cl => cl.PropertyName)
                .HasMaxLength(50)
                .IsUnicode(false);
        }
    }
}
