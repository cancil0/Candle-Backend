using Candle.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Candle.InfraStructure.Persistence.Configuration
{
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.Property(s => s.PostId).IsRequired();
            builder.Property(s => s.TagName).IsRequired();
        }
    }
}
