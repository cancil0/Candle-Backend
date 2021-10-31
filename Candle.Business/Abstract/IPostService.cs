using Candle.Common.Result;
using Candle.Model.DTOs.RequestDto.Post;
using Candle.Model.DTOs.ResponseDto.PostResponseDto;
using Candle.Model.Entities;
using System;
using System.Linq;

namespace Candle.Business.Abstract
{
    public interface IPostService
    {
        IDataResult<Post> GetById(Guid Id);

        IDataResult<IQueryable<GetPostResponseDto>> GetByUserName(GetPostByUserNameDto getPostByUserNameDto);

        IDataResult<IQueryable<GetPostResponseDto>> GetMainPost(string userName);

        IDataResult<IQueryable<GetPostResponseDto>> GetDiscoveryPost(string userName);

        IDataResult<Post> AddPost(AddPostDto addPostDto);

    }
}
