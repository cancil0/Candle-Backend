using Candle.Business.Abstract;
using Candle.Common.Result;
using Candle.DataAccess.Abstract;
using Candle.DataAccess.Service;
using Candle.InfraStructure.Persistence;
using Candle.Model.DTOs.RequestDto.Post;
using Candle.Model.DTOs.ResponseDto.CommentResponseDto;
using Candle.Model.DTOs.ResponseDto.LikeResponseDto;
using Candle.Model.DTOs.ResponseDto.MediaResponseDto;
using Candle.Model.DTOs.ResponseDto.PostResponseDto;
using Candle.Model.DTOs.ResponseDto.TagResponseDto;
using Candle.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Candle.Business.Service
{
    public class PostService : IPostService
    {
        private readonly IPostDal postDal;
        private readonly IUserDal userDal;
        private readonly IFollowerDal followerDal;
        private readonly CandleDbContext dbContext;
        public PostService()
        {
            dbContext = new CandleDbContext();
            dbContext.ChangeTracker.AutoDetectChangesEnabled = true;
            postDal = new PostDalService(dbContext);
            userDal = new UserDalService(dbContext);
            followerDal = new FollowerDalService(dbContext);
        }

        public IDataResult<List<GetPostResponseDto>> GetMainPost(GetPostByUserNameDto getPostByUserNameDto)
        {
            var followings = followerDal.GetFollowing(getPostByUserNameDto.UserName);
            int take = getPostByUserNameDto.TakeCount;
            int skip = (0 + getPostByUserNameDto.ScrollCount) * getPostByUserNameDto.TakeCount;
            List<GetPostResponseDto> result = postDal.GetMany(x => followings.Contains(x.User),
                                                                x => x.Medias,
                                                                x => x.Comments,
                                                                x => x.Likes,
                                                                x => x.Tags).Select(p => new GetPostResponseDto
                                                                {
                                                                    Id = p.Id,
                                                                    UserName = p.User.UserName,
                                                                    Content = p.Content,
                                                                    CreateTime = p.CreateTime,
                                                                    Likes = p.Likes.Select(x => new LikePostResponseDto
                                                                    {
                                                                        UserName = x.User.UserName,
                                                                        IsLiked = x.IsLiked
                                                                    }).ToList(),
                                                                    Comments = p.Comments.Select(x => new CommentPostResponseDto
                                                                    {
                                                                        CommentText = x.CommentText,
                                                                        UserName = x.User.UserName
                                                                    }).ToList(),
                                                                    Medias = p.Medias.Select(x => new MediaPostResponseDto
                                                                    {
                                                                        Caption = x.Caption,
                                                                        FileName = x.FileName
                                                                    }).ToList(),
                                                                    Tags = p.Tags.Select(x => new TagPostResponseDto
                                                                    { TagName = x.TagName }).ToList()
                                                                }).OrderByDescending(x => x.CreateTime).Skip(skip).Take(take).ToList();
            return new SuccessDataResult<List<GetPostResponseDto>>(result);
        }

        public IDataResult<List<GetPostResponseDto>> GetDiscoveryPost(GetPostByUserNameDto getPostByUserNameDto)
        {
            var notFollowings = followerDal.GetNotFollowing(getPostByUserNameDto.UserName).ToList();
            int take = getPostByUserNameDto.TakeCount;
            int skip = (0 + getPostByUserNameDto.ScrollCount) * getPostByUserNameDto.TakeCount;
            List<GetPostResponseDto> result = postDal.GetMany(x => notFollowings.Contains(x.User),
                                                                x => x.Medias,
                                                                x => x.Comments,
                                                                x => x.Likes,
                                                                x => x.Tags).Select(p => new GetPostResponseDto
                                                                {
                                                                    Id = p.Id,
                                                                    UserName = p.User.UserName,
                                                                    Content = p.Content,
                                                                    CreateTime = p.CreateTime,
                                                                    Likes = p.Likes.Select(x => new LikePostResponseDto
                                                                    {
                                                                        UserName = x.User.UserName,
                                                                        IsLiked = x.IsLiked
                                                                    }).ToList(),
                                                                    Comments = p.Comments.Select(x => new CommentPostResponseDto
                                                                    {
                                                                        CommentText = x.CommentText,
                                                                        UserName = x.User.UserName
                                                                    }).ToList(),
                                                                    Medias = p.Medias.Select(x => new MediaPostResponseDto
                                                                    {
                                                                        Caption = x.Caption,
                                                                        FileName = x.FileName
                                                                    }).ToList(),
                                                                    Tags = p.Tags.Select(x => new TagPostResponseDto
                                                                    { TagName = x.TagName }).ToList()
                                                                }).OrderByDescending(x => x.CreateTime).Skip(skip).Take(take).ToList();
            
            return new SuccessDataResult<List<GetPostResponseDto>>(result);
        }

        public IDataResult<GetPostResponseDto> GetById(Guid Id)
        {
            var post = postDal.Get(x => x.Id == Id,
                                     x => x.Medias,
                                     x => x.Comments,
                                     x => x.Likes,
                                     x => x.Tags,
                                     x => x.User);

            if(post == null)
                return new ErrorDataResult<GetPostResponseDto>();

            GetPostResponseDto postResponseDto = new()
            {
                Id = post.Id,
                UserName = post.User.UserName,
                Content = post.Content,
                CreateTime = post.CreateTime,
                Likes = post.Likes.Select(x => new LikePostResponseDto
                {
                    UserName = x.User.UserName,
                    IsLiked = x.IsLiked
                }).ToList(),
                Comments = post.Comments.Select(x => new CommentPostResponseDto
                {
                    CommentText = x.CommentText,
                    UserName = x.User.UserName
                }).ToList(),
                Medias = post.Medias.Select(x => new MediaPostResponseDto
                {
                    Caption = x.Caption,
                    FileName = x.FileName
                }).ToList(),
                Tags = post.Tags.Select(x => new TagPostResponseDto
                { TagName = x.TagName }).ToList()

            };
            return new SuccessDataResult<GetPostResponseDto>(postResponseDto);
        }

        public IDataResult<IQueryable<GetPostResponseDto>> GetByUserName(GetPostByUserNameDto getPostByUserNameDto)
        {
            int take = getPostByUserNameDto.TakeCount;
            int skip = (0 + getPostByUserNameDto.ScrollCount) * getPostByUserNameDto.TakeCount;
            IQueryable<GetPostResponseDto> result = postDal.GetMany(x => x.User.UserName == getPostByUserNameDto.UserName,
                                                                    x => x.Medias,
                                                                    x => x.Comments,
                                                                    x => x.Likes,
                                                                    x => x.Tags).Select(p => new GetPostResponseDto
                                                                                {
                                                                                    Id = p.Id,
                                                                                    UserName = p.User.UserName,
                                                                                    Content = p.Content,
                                                                                    CreateTime = p.CreateTime,
                                                                                    Likes = p.Likes.Select(x => new LikePostResponseDto
                                                                                    {
                                                                                        UserName = x.User.UserName,
                                                                                        IsLiked = x.IsLiked
                                                                                    }).ToList(),
                                                                                    Comments = p.Comments.Select(x => new CommentPostResponseDto
                                                                                    {
                                                                                        CommentText = x.CommentText,
                                                                                        UserName = x.User.UserName
                                                                                    }).ToList(),
                                                                                    Medias = p.Medias.Select(x => new MediaPostResponseDto
                                                                                    {
                                                                                        Caption = x.Caption,
                                                                                        FileName = x.FileName
                                                                                    }).ToList(),
                                                                                    Tags = p.Tags.Select(x => new TagPostResponseDto
                                                                                    { TagName = x.TagName }).ToList()
                                                                                }).OrderByDescending(x => x.CreateTime)
                                                                                  .Skip(skip)
                                                                                  .Take(take);

            return new SuccessDataResult<IQueryable<GetPostResponseDto>>(result);
        }

        public IDataResult<Post> AddPost(AddPostDto addPostDto)
        {
            if (addPostDto == null)
            {
                return new ErrorDataResult<Post>();
            }

            var user = userDal.Get(x => x.UserName == addPostDto.UserName);

            if(user == null)
            {
                return new ErrorDataResult<Post>();
            }

            Post post = new()
            {
                Content = addPostDto.Content,
                UserId = user.Id,
                Tags = addPostDto.Tags,
                Medias = addPostDto.Medias
            };

            postDal.Insert(post);

            return new SuccessDataResult<Post>(post);
        }

        public IResult UpdatePost()
        {
            return new SuccessResult();
        }

        public IResult DeletePost(Guid Id)
        {
            var post = postDal.Get(x => x.Id == Id);

            if (post == null)
                return new ErrorDataResult<GetPostResponseDto>();

            postDal.HardDelete(post);

            return new SuccessResult();
        }

        public int GetPostCount(string userName)
        {
            return postDal.GetMany(x => x.User.UserName == userName).Count();
        }
    }
}
