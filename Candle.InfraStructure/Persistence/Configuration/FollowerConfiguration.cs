using Candle.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candle.InfraStructure.Persistence.Configuration
{
    class FollowerConfiguration : IEntityTypeConfiguration<Follower>
    {
        public void Configure(EntityTypeBuilder<Follower> builder)
        {
            builder.Property(s => s.Id).IsRequired();
            builder.Property(s => s.UserId).IsRequired();
            builder.Property(s => s.FollowerId).IsRequired();
        }
    }
}
