using Candle.Common.Result;
using Candle.Model.DTOs.RequestDto.Follower;
using Candle.Model.Entities;
using System.Linq;

namespace Candle.Business.Abstract
{
    public interface IFollowerService
    {
        IDataResult<Follower> GetFollower(FollowerRequestDto followerRequestDto);
        IDataResult<IQueryable<User>> GetFollowers(string userName);
        IDataResult<IQueryable<User>> GetFollowings(string userName);
        IDataResult<IQueryable<User>> GetNotFollowings(string userName);
        IResult Follow(FollowerRequestDto followerRequestDto);
        IResult StopFollowing(FollowerRequestDto followerRequestDto);
        public int GetFollowingCount(string userName);
        public int GetFollowerCount(string userName);
    }
}
