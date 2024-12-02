using Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.Identity;
internal sealed class AppRoleConfiguration : IEntityTypeConfiguration<AppRole>
{
    public void Configure(EntityTypeBuilder<AppRole> builder)
    {
        builder.ToTable(nameof(AppRole));

        builder.HasKey(x => x.Id);

        // Each User can have many RoleClaims
        //builder.HasMany(e => e.Claims)
        //    .WithOne()
        //    .HasForeignKey(uc => uc.RoleId)
        //    .OnDelete(DeleteBehavior.Cascade);

        // Each User can have many entries in the UserRole join table
        //builder.HasMany(e => e.Users)
        //    .WithOne(u => u.Role)
        //    .HasForeignKey(u => u.RoleId)
        //    .OnDelete(DeleteBehavior.SetNull);
    }
}
