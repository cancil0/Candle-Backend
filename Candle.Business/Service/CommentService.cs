using Candle.Business.Abstract;
using Candle.Common.Result;
using Candle.DataAccess.Abstract;
using Candle.DataAccess.Service;
using Candle.InfraStructure.Persistence;
using Candle.Model.DTOs.CommentResponseDto.CommentResponseDto;
using Candle.Model.DTOs.RequestDto.Comment;
using Candle.Model.DTOs.ResponseDto.CommentResponseDto;
using Candle.Model.Entities;
using System;
using System.Collections.Generic;

namespace Candle.Business.Service
{
    public class CommentService : ICommentService
    {
        private readonly CandleDbContext dbContext;
        private readonly ICommentDal commentDal;
        private readonly IUserDal userDal;
        public CommentService()
        {
            dbContext = new CandleDbContext();
            commentDal = new CommentDalService(dbContext);
            userDal = new UserDalService(dbContext);
        }

        public DataResult<AddCommentResponseDto> AddComment(AddCommentRequestDto commentRequestDto)
        {
            Comment comment = new()
            {
                Id = Guid.NewGuid(),
                CommentText = commentRequestDto.CommentText,
                UserId = commentRequestDto.UserId,
                PostId = commentRequestDto.PostId,
                ParentCommentId = commentRequestDto.ParentCommentId
            };

            commentDal.Insert(comment);

            var user = userDal.GetbyId(comment.UserId);

            AddCommentResponseDto commentResponseDto = new() 
            {
                CommentId = comment.Id,
                UserProfilePhoto = user.ProfilePhotoPath 
            }; 

            return new SuccessDataResult<AddCommentResponseDto>(commentResponseDto);
        }

        public Result DeleteComment(Guid id)
        {
            Comment comment = commentDal.GetbyId(id);

            if (comment == null)
                return new ErrorDataResult<List<CommentPostResponseDto>>();
            
            commentDal.HardDelete(comment);
            return new SuccessResult();
        }

        public DataResult<List<CommentPostResponseDto>> GetPostComments(Guid postId)
        {
            var comments = commentDal.GetMany(x => x.PostId == postId, x => x.User);

            if(comments == null)
            {
                return new ErrorDataResult<List<CommentPostResponseDto>>();
            }

            List<CommentPostResponseDto> postComments = new();

            foreach (var comment in comments)
            {
                postComments.Add(new CommentPostResponseDto() 
                {
                    CommentId = comment.Id,
                    ParentCommentId = comment.ParentCommentId,
                    CommentText = comment.CommentText,
                    UserName = comment.User.UserName,
                    UserProfilePhoto = comment.User.ProfilePhotoPath,
                    Time = comment.CreateTime
                });
            }
            
            return new SuccessDataResult<List<CommentPostResponseDto>>(postComments);
        }
    }
}
