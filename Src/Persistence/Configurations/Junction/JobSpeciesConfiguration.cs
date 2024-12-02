using Domain.Entities.JunctionEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.Junction;

internal sealed class JobSpeciesConfiguration : IEntityTypeConfiguration<SpeciesJob>
{
    public void Configure(EntityTypeBuilder<SpeciesJob> builder)
    {
        builder.ToTable(nameof(SpeciesJob));

        builder.HasKey(x => new { x.JobId, x.SpeciesId });

        builder.HasOne(x => x.Species)
            .WithMany(a => a.JobSpecieses)
            .HasForeignKey(x => x.SpeciesId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Job)
            .WithMany(a => a.SpeciesJobs)
            .HasForeignKey(x => x.JobId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
