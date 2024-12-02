using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

internal sealed class ServiceAppoinmentConfiguration : IEntityTypeConfiguration<ServiceAppointment>
{
    public void Configure(EntityTypeBuilder<ServiceAppointment> builder)
    {
        builder.ToTable(nameof(ServiceAppointment));

        builder.Property(x => x.StartTime)
            .IsRequired()
            .HasColumnType("datetime2(0)");

        builder.Property(x => x.EndTime)
            .IsRequired()
            .HasColumnType("datetime2(0)");

        builder.HasOne(x => x.PetCenterService)
            .WithMany(p => p.ServiceAppointments)
            .HasForeignKey(p => p.PetCenterServiceId)
            .OnDelete(DeleteBehavior.Cascade);

        //builder.HasMany(x => x.Schedules)
        //    .WithOne(s => s.ServiceAppointment)
        //    .HasForeignKey(s => s.ServiceAppointmentId)
        //    .OnDelete(DeleteBehavior.Cascade);
    }
}
