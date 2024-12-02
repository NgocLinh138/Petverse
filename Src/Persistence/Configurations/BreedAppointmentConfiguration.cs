using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;
internal sealed class BreedAppointmentConfiguration : IEntityTypeConfiguration<BreedAppointment>
{
    public void Configure(EntityTypeBuilder<BreedAppointment> builder)
    {
        builder.ToTable(nameof(BreedAppointment));

        builder.HasOne(x => x.CenterBreed)
            .WithMany(p => p.BreedAppointments)
            .HasForeignKey(p => p.CenterBreedId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
