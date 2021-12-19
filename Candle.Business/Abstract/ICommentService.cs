using Candle.Common.Result;
using Candle.Model.DTOs.CommentResponseDto.CommentResponseDto;
using Candle.Model.DTOs.RequestDto.Comment;
using Candle.Model.DTOs.ResponseDto.CommentResponseDto;
using System;
using System.Collections.Generic;

namespace Candle.Business.Abstract
{
    public interface ICommentService
    {
        DataResult<AddCommentResponseDto> AddComment(AddCommentRequestDto commentRequestDto);

        Result DeleteComment(Guid id);

        DataResult<List<CommentPostResponseDto>> GetPostComments(Guid postId);

    }
}
