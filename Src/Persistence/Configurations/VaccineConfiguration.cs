using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

internal sealed class VaccineConfiguration : IEntityTypeConfiguration<Vaccine>
{
    public void Configure(EntityTypeBuilder<Vaccine> builder)
    {
        builder.ToTable(nameof(Vaccine));
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Species)
            .WithMany(s => s.Vaccines)
            .HasForeignKey(x => x.SpeciesId)
            .OnDelete(DeleteBehavior.Cascade);

        //builder.HasMany(X => X.VaccineRecommendations)
        //    .WithOne(v => v.Vaccine)
        //    .HasForeignKey(v => v.VaccineId)
        //    .OnDelete(DeleteBehavior.Cascade);
    }
}
