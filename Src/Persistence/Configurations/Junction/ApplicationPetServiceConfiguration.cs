using Domain.Entities.Junction;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.Junction;

internal sealed class ApplicationPetServiceConfiguration : IEntityTypeConfiguration<ApplicationPetService>
{
    public void Configure(EntityTypeBuilder<ApplicationPetService> builder)
    {
        builder.ToTable(nameof(ApplicationPetService));

        builder.HasKey(x => new { x.ApplicationId, x.PetServiceId });

        builder.HasOne(x => x.Application)
            .WithMany(a => a.ApplicationPetServices)
            .HasForeignKey(x => x.ApplicationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.PetService)
            .WithMany(a => a.ApplicationPetServices)
            .HasForeignKey(x => x.PetServiceId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
