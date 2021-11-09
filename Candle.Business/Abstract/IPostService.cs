using Candle.Common.Result;
using Candle.Model.DTOs.RequestDto.Post;
using Candle.Model.DTOs.ResponseDto.PostResponseDto;
using Candle.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Candle.Business.Abstract
{
    public interface IPostService
    {
        IDataResult<GetPostResponseDto> GetById(Guid Id);

        IDataResult<IQueryable<GetPostResponseDto>> GetByUserName(GetPostByUserNameDto getPostByUserNameDto);

        IDataResult<List<GetPostResponseDto>> GetMainPost(GetPostByUserNameDto getPostByUserNameDto);

        IDataResult<List<GetPostResponseDto>> GetDiscoveryPost(GetPostByUserNameDto getPostByUserNameDto);

        IDataResult<Post> AddPost(AddPostDto addPostDto);

        IResult DeletePost(Guid Id);

        int GetPostCount(string userName);

    }
}
