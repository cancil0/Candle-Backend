using Candle.DataAccess.Abstract;
using Candle.InfraStructure.Persistence;
using Candle.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Candle.DataAccess.Service
{
    public class FollowerDalService : IFollowerDal
    {
        private readonly CandleDbContext dbContext;
        private readonly IUserDal userDal;
        private DbSet<Follower> entities;
        public FollowerDalService(CandleDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.entities = dbContext.Set<Follower>();
            this.userDal = new UserDalService(dbContext);
        }

        public Follower Get(Expression<Func<Follower, bool>> expression)
        {
            var follower = entities.FirstOrDefault(expression);
            return follower;
        }

        public IQueryable<User> GetFollowers(string userName)
        {
            var followers = entities.Where(x => x.UserFollower.UserName == userName).Select(x=> x.User);
            return followers;
        }

        public IQueryable<User> GetFollowing(string userName)
        {
            var followers = entities.Where(x => x.User.UserName == userName).Select(x => x.UserFollower);
            return followers;
        }

        public IQueryable<User> GetNotFollowing(string userName)
        {
            var followers = entities.Where(x => x.User.UserName == userName).Select(x => x.FollowerId).ToList();
            var users = userDal.GetMany(x => !followers.Contains(x.Id) && x.UserName != userName).Take(50);
            return users;
        }

        public void Insert(Follower follower)
        {
            entities.Add(follower);
            dbContext.SaveChanges();
        }

        public void Delete(Follower follower)
        {
            entities.Remove(follower);
            dbContext.SaveChanges();
        }

    }
}
