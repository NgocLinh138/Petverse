using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

internal sealed class PetCenterServiceConfiguration : IEntityTypeConfiguration<PetCenterService>
{
    public void Configure(EntityTypeBuilder<PetCenterService> builder)
    {
        builder.ToTable(nameof(PetCenterService));

        builder.HasKey(x => x.Id);

        builder.Property(a => a.Price)
                   .HasColumnType("decimal(18, 2)");

        builder.HasIndex(x => new { x.PetServiceId, x.Rate })
            .HasDatabaseName("IX_PetServiceId_Rate")
            .IsDescending([false, true])
            .IsUnique(false);

        builder.HasOne(x => x.PetCenter)
            .WithMany(u => u.PetCenterServices)
            .HasForeignKey(x => x.PetCenterId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.PetService)
            .WithMany(u => u.PetCenterServices)
            .HasForeignKey(x => x.PetServiceId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
