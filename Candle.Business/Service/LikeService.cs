using Candle.Business.Abstract;
using Candle.Common.Result;
using Candle.DataAccess.Abstract;
using Candle.DataAccess.Service;
using Candle.InfraStructure.Persistence;
using Candle.Model.DTOs.RequestDto.Like;
using Candle.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Candle.Business.Service
{
    public class LikeService : ILikeService
    {
        private readonly ILikeDal likeDal;
        private readonly CandleDbContext dbContext;
        public LikeService()
        {
            dbContext = new CandleDbContext();
            likeDal = new LikeDalService(dbContext);
        }

        public IResult LikePost(LikePostRequestDto likePostRequest)
        {
            var like = likeDal.Get(x => x.UserId == likePostRequest.UserId && x.PostId == likePostRequest.PostId);

            if(like != null)
                return new ErrorResult();
            
            Like likeModel = new()
            {
                UserId = likePostRequest.UserId,
                PostId = likePostRequest.PostId,
                IsLiked = true
            };

            likeDal.Insert(likeModel);

            return new SuccessResult();
        }

        public IResult StopLikePost(LikePostRequestDto likePostRequest)
        {
            var like = likeDal.Get(x => x.UserId == likePostRequest.UserId && x.PostId == likePostRequest.PostId);

            if (like == null)
                return new ErrorResult(); ;

            likeDal.HardDelete(like);

            return new SuccessResult();
        }

        public int PostLikedCount(Guid postId)
        {
            return likeDal.GetMany(x => x.PostId == postId).Count();
        }

        public IDataResult<List<string>> GetWhoLikedPost(Guid postId)
        {
            var like = likeDal.GetMany(x => x.PostId == postId).Select(x => x.User.UserName);
            if (like == null)
                return new ErrorDataResult<List<string>>();

            return new SuccessDataResult<List<string>>(like.ToList());
        }
    }
}
