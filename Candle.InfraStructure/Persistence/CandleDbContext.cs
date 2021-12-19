using Candle.Model.Common;
using Candle.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq;
using System.Reflection;

namespace Candle.InfraStructure.Persistence
{
    public class CandleDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("User ID=postgres;Password=123456;Server=localhost;Port=5432;Database=Candle;Integrated Security=true;Pooling=true;");
            optionsBuilder.EnableSensitiveDataLogging();
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(f => f.Id);

            modelBuilder.Entity<Post>()
                .HasKey(f => f.UserId);

            modelBuilder.Entity<Post>()
                .HasKey(f => f.Id);

            modelBuilder.Entity<Comment>()
                .HasKey(f => new { f.UserId, f.PostId });

            modelBuilder.Entity<Comment>()
                .HasKey(f => f.Id);

            modelBuilder.Entity<Tag>()
                .HasKey(f => f.PostId);

            modelBuilder.Entity<Tag>()
                .HasKey(f => f.Id);

            modelBuilder.Entity<Like>()
                .HasKey(f => new { f.UserId, f.PostId });

            modelBuilder.Entity<Like>()
                .HasKey(f => f.Id);

            modelBuilder.Entity<Message>()
                .HasKey(f => f.UserId);

            modelBuilder.Entity<Message>()
               .HasKey(f => f.Id);

            modelBuilder.Entity<Media>()
                .HasKey(f => f.PostId);

            modelBuilder.Entity<Media>()
                .HasKey(f => f.Id);

            modelBuilder.Entity<PinForgotPassword>()
                .HasKey(f => f.UserId);

            modelBuilder.Entity<Follower>()
                .HasKey(f => f.Id);

            modelBuilder.Entity<ProfileStatusDef>()
                .HasKey(f => f.Key);

            modelBuilder.Entity<Follower>()
                .HasIndex(f => new { f.UserId, f.FollowerId }).IsUnique();

            modelBuilder.Entity<Follower>()
                .HasOne(sc => sc.UserFollower)
                .WithMany(x => x.Followers)
                .HasForeignKey(x => x.FollowerId);

            modelBuilder.Entity<Follower>()
                .HasOne(sc => sc.User)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.UserId);

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

            modelBuilder.Entity<User>()
                .HasOne(f => f.ProfileStatusDef)
                .WithMany(f => f.User)
                .HasForeignKey(f => f.ProfileStatus);

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
                    ((BaseEntity)entityEntry.Entity).Id = Guid.NewGuid();
                    ((BaseEntity)entityEntry.Entity).CreateTime = DateTime.Now;
                    ((BaseEntity)entityEntry.Entity).IsActive = 1;
                    ((BaseEntity)entityEntry.Entity).CreatedBy = Environment.MachineName;
                }

                if (entityEntry.State == EntityState.Modified)
                {
                    ((BaseEntity)entityEntry.Entity).UpdateTime = DateTime.Now;
                    ((BaseEntity)entityEntry.Entity).UpdatedBy = Environment.MachineName;
                }  
            }

            return base.SaveChanges();
        }

        public override EntityEntry<TEntity> Update<TEntity>(TEntity entity) where TEntity : class
        {
            if (entity == null)
            {
                throw new Exception("");
            }

            var type = entity.GetType();
            var et = this.Model.FindEntityType(type);
            var key = et.FindPrimaryKey();

            var keys = new object[key.Properties.Count];
            var x = 0;
            foreach (var keyName in key.Properties)
            {
                var keyProperty = type.GetProperty(keyName.Name, BindingFlags.Public | BindingFlags.Instance);
                keys[x++] = keyProperty.GetValue(entity);
            }

            var originalEntity = Find(type, keys);
            if (Entry(originalEntity).State == EntityState.Modified)
            {
                return base.Update(entity);
            }

            Entry(originalEntity).CurrentValues.SetValues(entity);
            return Entry((TEntity)originalEntity);
        }
    }
}
