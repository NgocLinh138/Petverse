using Domain.Entities.JunctionEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

internal sealed class SpeciesPlaceConfiguration : IEntityTypeConfiguration<SpeciesPlace>
{
    public void Configure(EntityTypeBuilder<SpeciesPlace> builder)
    {
        builder.ToTable(nameof(SpeciesPlace));

        builder.HasKey(x => new { x.PlaceId, x.SpeciesId });

        builder.HasOne(X => X.Place)
            .WithMany(u => u.SpeciesPlaces)
            .HasForeignKey(u => u.PlaceId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(X => X.Species)
            .WithMany(u => u.SpeciesPlaces)
            .HasForeignKey(u => u.SpeciesId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

