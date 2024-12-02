using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

internal sealed class SpeciesConfiguration : IEntityTypeConfiguration<Species>
{
    public void Configure(EntityTypeBuilder<Species> builder)
    {
        builder.ToTable(nameof(Species));

        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.Name)
            .IsUnique();

        //builder.HasMany(x => x.JobSpecieses)
        //    .WithOne(p => p.Species)
        //    .HasForeignKey(p => p.SpeciesId)
        //    .OnDelete(DeleteBehavior.Cascade);

        //builder.HasMany(x => x.Breeds)
        //    .WithOne(p => p.Species)
        //    .HasForeignKey(p => p.SpeciesId)
        //    .OnDelete(DeleteBehavior.Cascade);

        //builder.HasMany(x => x.Vaccines)
        //    .WithOne(p => p.Species)
        //    .HasForeignKey(p => p.SpeciesId)
        //    .OnDelete(DeleteBehavior.Cascade);

        //builder.HasMany(x => x.CenterBreeds)
        //    .WithOne(p => p.Species)
        //    .HasForeignKey(p => p.SpeciesId)
        //    .OnDelete(DeleteBehavior.Cascade);
    }
}
