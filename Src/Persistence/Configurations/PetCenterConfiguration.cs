using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

internal sealed class PetCenterConfiguration : IEntityTypeConfiguration<PetCenter>
{
    public void Configure(EntityTypeBuilder<PetCenter> builder)
    {
        builder.ToTable(nameof(PetCenter));

        builder.HasKey(x => x.Id);

        builder.Property(e => e.CreatedDate)
           .IsRequired()
           .HasColumnType("datetime2(0)");

        builder.Property(e => e.UpdatedDate)
           .HasColumnType("datetime2(0)");

        builder.Property(e => e.DeletedDate)
           .HasColumnType("datetime2(0)");

        builder.HasIndex(x => x.IsDeleted)
            .HasDatabaseName("IX_IsDeleted")
            .IsUnique(false);

        builder.HasOne(x => x.Application)
            .WithOne(u => u.PetCenter)
            .HasForeignKey<PetCenter>(x => x.ApplicationId)
            .OnDelete(DeleteBehavior.Cascade);

        //builder.HasOne(x => x.Job)
        //    .WithOne(j => j.PetCenter)
        //    .HasForeignKey<Job>(j => j.PetCenterId)
        //    .OnDelete(DeleteBehavior.Cascade);

        //builder.HasMany(x => x.Reports)
        //    .WithOne(c => c.PetCenter)
        //    .HasForeignKey(c => c.PetCenterId)
        //    .OnDelete(DeleteBehavior.Cascade);

        //builder.HasMany(x => x.CenterBreeds)
        //    .WithOne(c => c.PetCenter)
        //    .HasForeignKey(c => c.PetCenterId)
        //    .OnDelete(DeleteBehavior.Cascade);

        //builder.HasMany(x => x.PetCenterServices)
        //    .WithOne(p => p.PetCenter)
        //    .HasForeignKey(c => c.PetCenterId)
        //    .OnDelete(DeleteBehavior.Cascade);
    }
}
