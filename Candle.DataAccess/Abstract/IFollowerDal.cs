using Candle.Model.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Candle.DataAccess.Abstract
{
    public interface IFollowerDal
    {
        Follower Get(Expression<Func<Follower, bool>> expression);
        IQueryable<User> GetFollowers(string userName);
        IQueryable<User> GetFollowing(string userName);
        IQueryable<User> GetNotFollowing(string userName);
        void Insert(Follower follower);
        void Delete(Follower follower);
    }
}
