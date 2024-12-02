using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

internal sealed class TrackingConfiguration : IEntityTypeConfiguration<Tracking>
{
    public void Configure(EntityTypeBuilder<Tracking> builder)
    {
        builder.ToTable(nameof(Tracking));

        builder.HasKey(t => t.Id);

        builder.Property(e => e.UploadedAt)
            .IsRequired()
            .HasColumnType("datetime2(0)");

        builder.HasOne(j => j.Schedule)
            .WithMany(u => u.Trackings)
            .HasForeignKey(j => j.ScheduleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
