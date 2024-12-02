using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

internal sealed class CenterBreedImageConfiguration : IEntityTypeConfiguration<CenterBreedImage>
{
    public void Configure(EntityTypeBuilder<CenterBreedImage> builder)
    {
        builder.ToTable(nameof(CenterBreedImage));

        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.CenterBreed)
            .WithMany(c => c.CenterBreedImages)
            .HasForeignKey(x => x.CenterBreedId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
