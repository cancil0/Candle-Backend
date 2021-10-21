using Candle.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Candle.InfraStructure.Persistence.Configuration
{
    public class LikeConfiguration : IEntityTypeConfiguration<Like>
    {
        public void Configure(EntityTypeBuilder<Like> builder)
        {
            builder.Property(s => s.PostId).IsRequired();
            builder.Property(s => s.UserId).IsRequired();
            builder.Property(s => s.IsLiked).IsRequired();
        }
    }
}
