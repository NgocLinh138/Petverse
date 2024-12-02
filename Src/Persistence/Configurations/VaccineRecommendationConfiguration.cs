using Domain.Entities.JunctionEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

internal sealed class VaccineRecommendationConfiguration : IEntityTypeConfiguration<VaccineRecommendation>
{
    public void Configure(EntityTypeBuilder<VaccineRecommendation> builder)
    {
        builder.ToTable(nameof(VaccineRecommendation));

        builder.HasKey(x => x.Id);

        builder.HasIndex(x => new { x.PetId, x.VaccineId })
            .HasDatabaseName("IX_PetId_VaccineId")
            .IsUnique(false);

        builder.HasOne(x => x.Vaccine)
            .WithMany(x => x.VaccineRecommendations)
            .HasForeignKey(x => x.VaccineId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Pet)
            .WithMany(x => x.VaccineRecommendations)
            .HasForeignKey(x => x.PetId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
