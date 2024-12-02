using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

internal class PetVaccinatedConfiguration : IEntityTypeConfiguration<PetVaccinated>
{
    public void Configure(EntityTypeBuilder<PetVaccinated> builder)
    {
        builder.ToTable(nameof(PetVaccinated));

        builder.HasKey(x => x.Id);
        builder.Property(e => e.DateVaccinated)
           .IsRequired()
           .HasColumnType("datetime2(0)");

        builder.HasOne(v => v.Pet)
            .WithMany(p => p.PetVaccinateds)
            .HasForeignKey(p => p.PetId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
