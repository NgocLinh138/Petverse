using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

internal sealed class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
{
    public void Configure(EntityTypeBuilder<Appointment> builder)
    {
        builder.ToTable(nameof(Appointment));

        builder.HasKey(x => x.Id);

        builder.Property(e => e.StartTime)
            .IsRequired()
            .HasColumnType("datetime2(0)");

        builder.Property(e => e.EndTime)
            .IsRequired()
            .HasColumnType("datetime2(0)");

        builder.Property(e => e.CreatedDate)
            .IsRequired()
            .HasColumnType("datetime2(0)");

        builder.Property(e => e.UpdatedDate)
            .HasColumnType("datetime2(0)");

        builder.Property(a => a.Amount)
            .HasColumnType("decimal(18, 2)");

        builder.HasIndex(x => x.CreatedDate)
            .HasDatabaseName("IX_CreatedDate")
            .IsUnique(false);

        builder.HasOne(x => x.User)
            .WithMany(u => u.Appointments)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Pet)
            .WithMany(p => p.Appointments)
            .HasForeignKey(x => x.PetId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.PetCenter)
            .WithMany(p => p.Appointments)
            .HasForeignKey(x => x.PetCenterId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.UseTptMappingStrategy();
    }
}
