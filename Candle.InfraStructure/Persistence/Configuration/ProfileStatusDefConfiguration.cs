using Candle.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Candle.InfraStructure.Persistence.Configuration
{
    public class ProfileStatusDefConfiguration : IEntityTypeConfiguration<ProfileStatusDef>
    {
        public void Configure(EntityTypeBuilder<ProfileStatusDef> builder)
        {
            builder.Property(s => s.Key).IsRequired();
            builder.Property(s => s.Value).IsRequired();
        }
    }
}
