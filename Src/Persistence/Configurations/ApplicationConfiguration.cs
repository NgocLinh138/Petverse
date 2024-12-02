using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

internal sealed class ApplicationConfiguration : IEntityTypeConfiguration<Application>
{
    public void Configure(EntityTypeBuilder<Application> builder)
    {
        builder.ToTable(nameof(Application));

        builder.HasKey(a => a.Id);

        builder.Property(x => x.PhoneNumber)
            .HasMaxLength(15);

        builder.Property(e => e.CreatedDate)
            .IsRequired()
            .HasColumnType("datetime2(0)");

        builder.Property(e => e.UpdatedDate)
            .HasColumnType("datetime2(0)");

        builder.HasOne(j => j.User)
            .WithOne(u => u.Application)
            .HasForeignKey<Application>(j => j.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        //builder.HasOne(j => j.PetCenter)
        //    .WithOne(u => u.Application)
        //    .HasForeignKey<PetCenter>(j => j.ApplicationId)
        //    .OnDelete(DeleteBehavior.NoAction);

        //builder.HasMany(x => x.ApplicationPetServices)
        //    .WithOne(x => x.Application)
        //    .HasForeignKey(x => x.ApplicationId)
        //    .OnDelete(DeleteBehavior.Cascade);

        //builder.HasMany(x => x.Certifications)
        //    .WithOne(x => x.Application)
        //    .HasForeignKey(x => x.ApplicationId)
        //    .OnDelete(DeleteBehavior.Cascade);
    }
}
