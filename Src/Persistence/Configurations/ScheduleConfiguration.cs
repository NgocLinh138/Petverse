using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

internal sealed class ScheduleConfiguration : IEntityTypeConfiguration<Schedule>
{
    public void Configure(EntityTypeBuilder<Schedule> builder)
    {
        builder.ToTable(nameof(Schedule));

        builder.HasKey(x => x.Id);

        builder.HasIndex(x => new { x.Date, x.Id })
            .HasDatabaseName("IX_Date_Id")
            .IsUnique(true);

        builder.HasOne(s => s.ServiceAppointment)
            .WithMany(p => p.Schedules)
            .HasForeignKey(p => p.ServiceAppointmentId)
            .OnDelete(DeleteBehavior.Cascade);

        //builder.HasMany(s => s.Trackings)
        //    .WithOne(t => t.Schedule)
        //    .HasForeignKey(s => s.ScheduleId)
        //    .OnDelete(DeleteBehavior.Cascade);
    }
}
