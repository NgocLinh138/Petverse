using Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.Identity;
internal sealed class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.ToTable(nameof(AppUser));

        builder.HasKey(x => x.Id);

        new AuditConfiguration<AppUser>().Configure(builder);

        builder.Property(e => e.DateOfBirth)
           .HasColumnType("datetime2(0)");

        builder.Property(a => a.Balance)
           .HasColumnType("decimal(18, 2)");

        builder.HasIndex(x => x.CreatedDate)
            .HasDatabaseName("IX_CreatedDate")
            .IsUnique(false);

        builder.HasOne(u => u.Role)
           .WithMany(x => x.Users)
           .HasForeignKey(p => p.RoleId)
           .OnDelete(DeleteBehavior.Cascade);
    }
}
