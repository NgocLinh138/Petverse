using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    internal sealed class ReportImageConfiguration : IEntityTypeConfiguration<ReportImage>
    {
        public void Configure(EntityTypeBuilder<ReportImage> builder)
        {
            builder.ToTable(nameof(ReportImage));

            builder.HasKey(x => x.Id);

            builder.Property(e => e.CreatedDate)
               .IsRequired()
               .HasColumnType("datetime2(0)");

            builder.Property(e => e.UpdatedDate)
               .HasColumnType("datetime2(0)");

            builder.HasOne(x => x.Report)
                .WithMany(c => c.ReportImages)
                .HasForeignKey(x => x.ReportId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
