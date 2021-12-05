using Candle.Common.Result;
using Candle.Model.DTOs.RequestDto.Like;
using System;
using System.Collections.Generic;

namespace Candle.Business.Abstract
{
    public interface ILikeService
    {
        IResult LikePost(LikePostRequestDto likePostRequest);

        IResult StopLikePost(LikePostRequestDto likePostRequest);

        IDataResult<List<string>> GetWhoLikedPost(Guid postId);

        int PostLikedCount(Guid postId);
    }
}
