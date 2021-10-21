using Candle.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Candle.InfraStructure.Persistence.Configuration
{
    public class MediaConfiguration : IEntityTypeConfiguration<Media>
    {
        public void Configure(EntityTypeBuilder<Media> builder)
        {
            builder.Property(s => s.PostId).IsRequired();
            builder.Property(s => s.MediaType).IsRequired();
            builder.Property(s => s.Caption).IsRequired();
            builder.Property(s => s.FileName).IsRequired();
            builder.Property(s => s.FileSize).IsRequired();
        }
    }
}
