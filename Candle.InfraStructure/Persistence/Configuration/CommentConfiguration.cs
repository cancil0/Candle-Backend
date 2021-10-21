using Candle.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Candle.InfraStructure.Persistence.Configuration
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.Property(s => s.PostId).IsRequired();
            builder.Property(s => s.UserId).IsRequired();
            builder.Property(s => s.CommentText).IsRequired().HasMaxLength(250);
        }
    }
}
