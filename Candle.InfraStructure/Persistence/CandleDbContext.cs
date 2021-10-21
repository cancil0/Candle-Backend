using Candle.Model.Common;
using Candle.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Candle.InfraStructure.Persistence
{
    public class CandleDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("User ID=postgres;Password=123456;Server=localhost;Port=5432;Database=Candle;Integrated Security=true;Pooling=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>()
                .HasKey(f => f.UserId);

            modelBuilder.Entity<Comment>()
                .HasKey(f => new { f.UserId, f.PostId });

            modelBuilder.Entity<Tag>()
                .HasKey(f => f.PostId);

            modelBuilder.Entity<Like>()
                .HasKey(f => new { f.UserId, f.PostId });

            modelBuilder.Entity<Message>()
                .HasKey(f => f.UserId);

            modelBuilder.Entity<Media>()
                .HasKey(f => f.PostId);

            modelBuilder.Entity<PinForgotPassword>()
                .HasKey(f => f.UserId);

            modelBuilder.Entity<Post>()
               .HasOne(f => f.User)
               .WithMany(f => f.Posts)
               .HasForeignKey(f => f.UserId);

            modelBuilder.Entity<Comment>()
               .HasOne(f => f.Post)
               .WithMany(f => f.Comments)
               .HasForeignKey(f => f.PostId);

            modelBuilder.Entity<Comment>()
               .HasOne(f => f.User)
               .WithMany(f => f.Comments)
               .HasForeignKey(f => f.UserId);

            modelBuilder.Entity<Tag>()
               .HasOne(f => f.Post)
               .WithMany(f => f.Tags)
               .HasForeignKey(f => f.PostId);

            modelBuilder.Entity<Like>()
               .HasOne(f => f.Post)
               .WithMany(f => f.Likes)
               .HasForeignKey(f => f.PostId);

            modelBuilder.Entity<Like>()
               .HasOne(f => f.User)
               .WithMany(f => f.Likes)
               .HasForeignKey(f => f.UserId);

            modelBuilder.Entity<Message>()
               .HasOne(f => f.User)
               .WithMany(f => f.Messages)
               .HasForeignKey(f => f.UserId);

            modelBuilder.Entity<Media>()
               .HasOne(f => f.Post)
               .WithMany(f => f.Medias)
               .HasForeignKey(f => f.PostId);

            modelBuilder.Entity<User>()
            .HasOne(f => f.PinForgotPassword)
            .WithOne(f => f.User)
            .HasForeignKey<PinForgotPassword>(f => f.UserId);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CandleDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (
                        e.State == EntityState.Added
                        || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).CreateTime = DateTime.Now;
                    ((BaseEntity)entityEntry.Entity).IsActive = 1;
                }

                if (entityEntry.State == EntityState.Modified)
                    ((BaseEntity)entityEntry.Entity).UpdateTime = DateTime.Now;
                    
            }

            return base.SaveChanges();
        }
    }
}
