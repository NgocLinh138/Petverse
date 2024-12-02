using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

internal sealed class PhotoConfiguration : IEntityTypeConfiguration<PetImage>
{
    public void Configure(EntityTypeBuilder<PetImage> builder)
    {
        builder.ToTable(nameof(PetImage));

        builder.HasKey(p => p.Id);

        builder.Property(e => e.CreatedDate)
           .IsRequired()
           .HasColumnType("datetime2(0)");

        builder.Property(e => e.UpdatedDate)
           .HasColumnType("datetime2(0)");

        builder.HasOne(x => x.Pet)
            .WithMany(p => p.Photos)
            .HasForeignKey(p => p.PetId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
