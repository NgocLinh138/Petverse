using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class JobConfiguration : IEntityTypeConfiguration<Job>
{
    public void Configure(EntityTypeBuilder<Job> builder)
    {
        builder.ToTable(nameof(Job));

        builder.HasKey(x => x.Id);

        builder.Property(x => x.CreatedDate)
           .IsRequired()
           .HasColumnType("datetime2(0)");

        builder.Property(x => x.UpdatedDate)
           .HasColumnType("datetime2(0)");

        builder.HasOne(x => x.PetCenter)
            .WithOne(p => p.Job)
            .HasForeignKey<Job>(x => x.PetCenterId)
            .OnDelete(DeleteBehavior.Cascade);

        //builder.HasMany(x => x.SpeciesJobs)
        //    .WithOne(t => t.Job)
        //    .HasForeignKey(x => x.JobId)
        //    .OnDelete(DeleteBehavior.Cascade);
    }
}
