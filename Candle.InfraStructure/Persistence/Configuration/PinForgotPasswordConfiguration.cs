using Candle.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Candle.InfraStructure.Persistence.Configuration
{
    public class PinForgotPasswordConfiguration : IEntityTypeConfiguration<PinForgotPassword>
    {
        public void Configure(EntityTypeBuilder<PinForgotPassword> builder)
        {
            builder.Property(s => s.Pin).IsRequired().HasMaxLength(6);
            builder.Property(s => s.UserId).IsRequired();
        }
    }
}
