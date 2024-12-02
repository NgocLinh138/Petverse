using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

internal sealed class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable(nameof(Transaction));
        builder.HasKey(x => x.Id);

        builder.Property(e => e.CreatedDate)
            .IsRequired()
            .HasColumnType("datetime2(0)");

        builder.Property(e => e.UpdatedDate)
            .HasColumnType("datetime2(0)");

        builder.Property(a => a.Amount)
            .HasColumnType("decimal(18, 2)");

        builder.HasOne(x => x.User)
            .WithMany(x => x.Payments)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Appointment)
            .WithMany(a => a.Transactions)
            .HasForeignKey(x => x.AppointmentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
