using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

internal sealed class PetConfiguration : IEntityTypeConfiguration<Pet>
{
    public void Configure(EntityTypeBuilder<Pet> builder)
    {
        builder.ToTable(nameof(Pet));

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Name)
            .HasMaxLength(20);

        new AuditConfiguration<Pet>().Configure(builder);

        builder.HasOne(p => p.User)
            .WithMany(u => u.Pets)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(p => p.Breed)
            .WithMany(t => t.Pets)
            .HasForeignKey(p => p.BreedId)
            .OnDelete(DeleteBehavior.Cascade);

        //builder.HasMany(p => p.ServiceAppointments)
        //    .WithOne(v => v.Pet)
        //    .HasForeignKey(v => v.PetId)
        //    .OnDelete(DeleteBehavior.Cascade);

        //builder.HasMany(x => x.Photos)
        //    .WithOne(p => p.Pet)
        //    .HasForeignKey(p => p.PetId)
        //    .OnDelete(DeleteBehavior.Cascade);

        //builder.HasMany(p => p.PetVaccinateds)
        //    .WithOne(v => v.Pet)
        //    .HasForeignKey(v => v.PetId)
        //    .OnDelete(DeleteBehavior.Cascade);

        //builder.HasMany(p => p.VaccineRecommendations)
        //    .WithOne(v => v.Pet)
        //    .HasForeignKey(v => v.PetId)
        //    .OnDelete(DeleteBehavior.Cascade);

        //builder.HasMany(p => p.Warnings)
        //    .WithOne(v => v.Pet)
        //    .HasForeignKey(v => v.PetId)
        //    .OnDelete(DeleteBehavior.Cascade);

        //builder.HasMany(p => p.BreedingAppointments)
        //    .WithOne(v => v.Pet)
        //    .HasForeignKey(v => v.PetId)
        //    .OnDelete(DeleteBehavior.Cascade);
    }
}
