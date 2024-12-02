using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

internal sealed class BreedConfiguration : IEntityTypeConfiguration<Breed>
{
    public void Configure(EntityTypeBuilder<Breed> builder)
    {
        builder.ToTable(nameof(Breed));

        builder.HasOne(x => x.Species)
           .WithMany(p => p.Breeds)
           .HasForeignKey(x => x.SpeciesId)
           .OnDelete(DeleteBehavior.NoAction); // cause cycles or multiple cascade paths
    }
}
