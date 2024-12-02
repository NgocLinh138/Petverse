using Domain.Abstractions.EntityBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

internal sealed class AuditConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
    where TEntity : class, IAudit
{
    public void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.Property(e => e.CreatedDate)
               .IsRequired()
               .HasColumnType("datetime2(0)");

        builder.Property(e => e.UpdatedDate)
               .HasColumnType("datetime2(0)");

        builder.Property(e => e.DeletedDate)
               .HasColumnType("datetime2(0)");

        builder.Property(e => e.IsDeleted)
               .IsRequired()
               .HasDefaultValue(false);
    }
}
