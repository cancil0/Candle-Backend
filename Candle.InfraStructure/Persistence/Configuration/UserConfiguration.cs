using Candle.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Candle.InfraStructure.Persistence.Configuration
{
    class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(s => s.Name).IsRequired();
            builder.Property(s => s.SurName).IsRequired();
            builder.Property(s => s.BirthDate).IsRequired();
            builder.Property(s => s.Email).IsRequired();
            builder.HasIndex(s => s.Email).IsUnique();
            builder.Property(s => s.UserName).IsRequired();
            builder.HasIndex(s => s.UserName).IsUnique();
            builder.Property(s => s.Password).IsRequired();
            builder.Property(s => s.MobilePhone).IsRequired().HasMaxLength(10);
            builder.HasIndex(s => s.MobilePhone).IsUnique();
            builder.Property(s => s.Gender).IsRequired().HasMaxLength(1);
        }
    }
}
