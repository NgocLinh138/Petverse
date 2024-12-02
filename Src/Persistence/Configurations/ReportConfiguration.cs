using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

internal sealed class ReportConfiguration : IEntityTypeConfiguration<Report>
{
    public void Configure(EntityTypeBuilder<Report> builder)
    {
        builder.ToTable(nameof(Report));
        builder.HasKey(x => x.Id);

        builder.Property(e => e.CreatedDate)
            .IsRequired()
            .HasColumnType("datetime2(0)");

        builder.Property(e => e.UpdatedDate)
            .HasColumnType("datetime2(0)");

        builder.HasOne(x => x.Appointment)
            .WithOne(a => a.Report)
            .HasForeignKey<Report>(x => x.AppointmentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
