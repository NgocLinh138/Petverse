using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

internal sealed class PetServiceConfiguration : IEntityTypeConfiguration<PetService>
{
    public void Configure(EntityTypeBuilder<PetService> builder)
    {
        builder.ToTable(nameof(PetService));

        builder.HasKey(x => x.Id);

        //builder.HasMany(x => x.ApplicationPetServices)
        //    .WithOne(c => c.PetService)
        //    .HasForeignKey(c => c.PetServiceId)
        //    .OnDelete(DeleteBehavior.Cascade);

        //builder.HasMany(x => x.PetCenterServices)
        //    .WithOne(c => c.PetService)
        //    .HasForeignKey(c => c.PetServiceId)
        //    .OnDelete(DeleteBehavior.Cascade);
    }
}
