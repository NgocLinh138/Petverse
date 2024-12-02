using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

internal sealed class CenterBreedConfiguration : IEntityTypeConfiguration<CenterBreed>
{
    public void Configure(EntityTypeBuilder<CenterBreed> builder)
    {
        builder.ToTable(nameof(CenterBreed));

        builder.HasKey(x => x.Id);

        builder.Property(a => a.Price)
           .HasColumnType("decimal(18, 2)");

        builder.Property(e => e.CreatedDate)
           .IsRequired()
           .HasColumnType("datetime2(0)");

        builder.Property(e => e.UpdatedDate)
           .HasColumnType("datetime2(0)");

        builder.HasOne(x => x.PetCenter)
            .WithMany(p => p.CenterBreeds)
            .HasForeignKey(x => x.PetCenterId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Species)
            .WithMany(p => p.CenterBreeds)
            .HasForeignKey(x => x.SpeciesId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
