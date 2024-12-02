using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

internal sealed class PetCenterRateConfiguration : IEntityTypeConfiguration<AppointmentRate>
{
    public void Configure(EntityTypeBuilder<AppointmentRate> builder)
    {
        builder.ToTable(nameof(AppointmentRate));

        builder.HasKey(x => x.Id);

        new AuditConfiguration<AppointmentRate>().Configure(builder);

        builder.HasOne(x => x.Appointment)
            .WithOne(a => a.AppointmentRate)
            .HasForeignKey<AppointmentRate>(x => x.AppointmentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
