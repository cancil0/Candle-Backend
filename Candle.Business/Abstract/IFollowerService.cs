using Candle.Common.Result;
using Candle.Model.DTOs.RequestDto.Follower;
using Candle.Model.DTOs.ResponseDto.FollowerResponseDto;
using Candle.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Candle.Business.Abstract
{
    public interface IFollowerService
    {
        IDataResult<Follower> GetFollower(FollowerRequestDto followerRequestDto);
        IDataResult<IQueryable<User>> GetFollowers(string userName);
        IDataResult<IQueryable<User>> GetFollowings(string userName);
        IDataResult<IQueryable<User>> GetNotFollowings(string userName);
        IDataResult<List<GetFollowerResponseDto>> GetFollowingList(string userName);
        IDataResult<List<GetFollowerResponseDto>> GetFollowerList(string userName);
        IDataResult<Guid> Follow(FollowerRequestDto followerRequestDto);
        IResult StopFollowing(FollowerRequestDto followerRequestDto);
        IResult StopFollowingById(Guid id);
        public int GetFollowingCount(string userName);
        public int GetFollowerCount(string userName);
    }
}
