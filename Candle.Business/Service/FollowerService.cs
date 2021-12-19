using Candle.Business.Abstract;
using Candle.Common.Result;
using Candle.DataAccess.Abstract;
using Candle.DataAccess.Service;
using Candle.InfraStructure.Persistence;
using Candle.Model.DTOs.RequestDto.Follower;
using Candle.Model.DTOs.ResponseDto.FollowerResponseDto;
using Candle.Model.Entities;
using System;
using System.Collections.Generic;
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

        public IDataResult<List<GetFollowerResponseDto>> GetFollowerList(string userName)
        {
            var result = followerDal.GetFollowerList(userName);
            List<GetFollowerResponseDto> followerList = new();
            foreach (var item in result)
            {
                followerList.Add(new GetFollowerResponseDto 
                { 
                    Id = item.Id,
                    FollowerId = item.UserId,
                    FollowerName = item.User.UserName
                });
            }

            return new SuccessDataResult<List<GetFollowerResponseDto>>(followerList.OrderBy(x => x.FollowerName).ToList());
        }

        public IDataResult<List<GetFollowerResponseDto>> GetFollowingList(string userName)
        {
            var result = followerDal.GetFollowingList(userName);
            List<GetFollowerResponseDto> followingList = new();
            foreach (var item in result)
            {
                followingList.Add(new GetFollowerResponseDto
                {
                    Id = item.Id,
                    FollowerId = item.FollowerId,
                    FollowerName = item.UserFollower.UserName
                });
            }
            return new SuccessDataResult<List<GetFollowerResponseDto>>(followingList.OrderBy(x => x.FollowerName).ToList());
        }

        public IDataResult<IQueryable<User>> GetNotFollowings(string userName)
        {
            var result = followerDal.GetNotFollowing(userName);
            return new SuccessDataResult<IQueryable<User>>(result);
        }

        public IDataResult<Guid> Follow(FollowerRequestDto followerRequestDto)
        {
            var followed = GetFollower(followerRequestDto);

            if (followed.Data != null)
            {
                return new ErrorDataResult<Guid>();
            }

            Follower follower = new() 
            { 
                Id = Guid.NewGuid(),
                UserId = followerRequestDto.UserId,
                FollowerId = followerRequestDto.FollowerId
            };

            followerDal.Insert(follower);
            return new SuccessDataResult<Guid>(follower.Id);
        }

        public IResult StopFollowing(FollowerRequestDto followerRequestDto)
        {
            var follower = GetFollower(followerRequestDto);

            followerDal.Delete(follower.Data);
            return new SuccessResult();
        }

        public IResult StopFollowingById(Guid id)
        {
            var follower = followerDal.Get(x => x.Id == id);

            followerDal.Delete(follower);
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
