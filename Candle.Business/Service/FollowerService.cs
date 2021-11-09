using Candle.Business.Abstract;
using Candle.Common.Result;
using Candle.DataAccess.Abstract;
using Candle.DataAccess.Service;
using Candle.InfraStructure.Persistence;
using Candle.Model.DTOs.RequestDto.Follower;
using Candle.Model.Entities;
using System;
using System.Linq;

namespace Candle.Business.Service
{
    public class FollowerService : IFollowerService
    {
        private readonly IFollowerDal followerDal;
        private readonly CandleDbContext dbContext;
        public FollowerService()
        {
            dbContext = new CandleDbContext();
            followerDal = new FollowerDalService(dbContext);
        }

        public IDataResult<Follower> GetFollower(FollowerRequestDto followerRequestDto)
        {
            var result = followerDal.Get(x => x.UserId == followerRequestDto.UserId && x.FollowerId == followerRequestDto.FollowerId);
            return new SuccessDataResult<Follower>(result);
        }

        public IDataResult<IQueryable<User>> GetFollowers(string userName)
        {
            var result = followerDal.GetFollowers(userName);
            return new SuccessDataResult<IQueryable<User>>(result);
        }

        public IDataResult<IQueryable<User>> GetFollowings(string userName)
        {
            var result = followerDal.GetFollowing(userName);
            return new SuccessDataResult<IQueryable<User>>(result);
        }

        public IDataResult<IQueryable<User>> GetNotFollowings(string userName)
        {
            var result = followerDal.GetNotFollowing(userName);
            return new SuccessDataResult<IQueryable<User>>(result);
        }

        public IResult Follow(FollowerRequestDto followerRequestDto)
        {
            var followed = GetFollower(followerRequestDto);

            if (followed.Data != null)
            {
                return new ErrorResult();
            }

            Follower follower = new() 
            { 
                Id = Guid.NewGuid(),
                UserId = followerRequestDto.UserId,
                FollowerId = followerRequestDto.FollowerId
            };

            followerDal.Insert(follower);
            return new SuccessResult();
        }

        public IResult StopFollowing(FollowerRequestDto followerRequestDto)
        {
            var follower = GetFollower(followerRequestDto);

            followerDal.Delete(follower.Data);
            return new SuccessResult();
        }

        public int GetFollowingCount(string userName)
        {
            return followerDal.GetFollowing(userName).Count();
        }

        public int GetFollowerCount(string userName)
        {
            return followerDal.GetFollowers(userName).Count();
        }
    }
}
