using Candle.Business.Abstract;
using Candle.Common.Result;
using Candle.DataAccess.Abstract;
using Candle.DataAccess.Service;
using Candle.InfraStructure.Persistence;
using Candle.Model.DTOs.CommentResponseDto.CommentResponseDto;
using Candle.Model.DTOs.RequestDto.Post;
using Candle.Model.DTOs.RequestDto.User;
using Candle.Model.DTOs.ResponseDto.LikeResponseDto;
using Candle.Model.DTOs.ResponseDto.MediaResponseDto;
using Candle.Model.DTOs.ResponseDto.PostResponseDto;
using Candle.Model.DTOs.ResponseDto.TagResponseDto;
using Candle.Model.Entities;
using Candle.Model.Enums;
using Candle.Model.Enums.EnumExtensions;
using Microsoft.EntityFrameworkCore;
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
        private readonly IUserService userService;
        private readonly CandleDbContext dbContext;
        public PostService()
        {
            dbContext = new CandleDbContext();
            dbContext.ChangeTracker.AutoDetectChangesEnabled = true;
            postDal = new PostDalService(dbContext);
            userDal = new UserDalService(dbContext);
            followerDal = new FollowerDalService(dbContext);
            userService = new UserService();
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
                                                                    UserId = p.User.Id,
                                                                    Content = p.Content,
                                                                    CreateTime = p.CreateTime,
                                                                    ProfilePhotoPath = p.User.ProfilePhotoPath,
                                                                    Likes = p.Likes.Select(x => new LikePostResponseDto
                                                                    {
                                                                        UserName = x.User.UserName,
                                                                        IsLiked = x.IsLiked
                                                                    }).ToList(),
                                                                    Comments = p.Comments.Select(x => new CommentPostResponseDto
                                                                    {
                                                                        CommentId = x.Id,
                                                                        ParentCommentId = x.ParentCommentId,
                                                                        UserName = x.User.UserName,
                                                                        UserProfilePhoto = x.User.ProfilePhotoPath,
                                                                        CommentText = x.CommentText,
                                                                        Time = x.CreateTime
                                                                    }).OrderBy(x => x.Time).ToList(),
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
            List<GetPostResponseDto> result = postDal.GetMany(x => notFollowings.Contains(x.User) && x.User.ProfileStatus == ProfileStatusKey.Everyone.GetValue(),
                                                                x => x.Medias,
                                                                x => x.Comments,
                                                                x => x.Likes,
                                                                x => x.Tags).Select(p => new GetPostResponseDto
                                                                {
                                                                    Id = p.Id,
                                                                    UserName = p.User.UserName,
                                                                    UserId = p.User.Id,
                                                                    Content = p.Content,
                                                                    CreateTime = p.CreateTime,
                                                                    ProfilePhotoPath = p.User.ProfilePhotoPath,
                                                                    Likes = p.Likes.Select(x => new LikePostResponseDto
                                                                    {
                                                                        UserName = x.User.UserName,
                                                                        IsLiked = x.IsLiked
                                                                    }).ToList(),
                                                                    Comments = p.Comments.Select(x => new CommentPostResponseDto
                                                                    {
                                                                        CommentId = x.Id,
                                                                        ParentCommentId = x.ParentCommentId,
                                                                        UserName = x.User.UserName,
                                                                        UserProfilePhoto = x.User.ProfilePhotoPath,
                                                                        CommentText = x.CommentText,
                                                                        Time = x.CreateTime
                                                                    }).OrderBy(x => x.Time).ToList(),
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
            var post = postDal.GetAll()
                                .Include(x => x.Medias)
                                .Include(x => x.Comments)
                                .Include(x => x.Likes)
                                .Include(x => x.Tags)
                                .Include(x => x.User)
                                .Include("Likes.User")
                                .Include("Comments.User")
                                .FirstOrDefault(x => x.Id == Id);
            
            if(post == null)
                return new ErrorDataResult<GetPostResponseDto>();

            GetPostResponseDto postResponseDto = new()
            {
                Id = post.Id,
                UserName = post.User.UserName,
                UserId = post.User.Id,
                Content = post.Content,
                CreateTime = post.CreateTime,
                ProfilePhotoPath = post.User.ProfilePhotoPath,
                Likes = post.Likes.Select(x => new LikePostResponseDto
                {
                    UserName = x.User.UserName,
                    IsLiked = x.IsLiked
                }).ToList(),
                Comments = post.Comments.Select(x => new CommentPostResponseDto
                {
                    CommentId = x.Id,
                    ParentCommentId = x.ParentCommentId,
                    UserName = x.User.UserName,
                    UserProfilePhoto = x.User.ProfilePhotoPath,
                    CommentText = x.CommentText,
                    Time = x.CreateTime
                }).OrderBy(x => x.Time).ToList(),
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
            var profileStatusRequest = new CheckUserProfileStatusRequestDto
            {
                UserName = getPostByUserNameDto.UserName,
                ProfileOwnerUserName = getPostByUserNameDto.LoggedInUserName
            };

            Result resultProfileStatus = userService.CheckUserProfileStatus(profileStatusRequest);

            if(!resultProfileStatus.IsSuccess)
            {
                return new ErrorDataResult<IQueryable<GetPostResponseDto>>(null, resultProfileStatus.Message);
            }
                
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
                                                                                    UserId = p.User.Id,
                                                                                    Content = p.Content,
                                                                                    CreateTime = p.CreateTime,
                                                                                    ProfilePhotoPath = p.User.ProfilePhotoPath,
                                                                                    Likes = p.Likes.Select(x => new LikePostResponseDto
                                                                                    {
                                                                                        UserName = x.User.UserName,
                                                                                        IsLiked = x.IsLiked
                                                                                    }).ToList(),
                                                                                    Comments = p.Comments.Select(x => new CommentPostResponseDto
                                                                                    {
                                                                                        CommentId = x.Id,
                                                                                        ParentCommentId = x.ParentCommentId,
                                                                                        UserName = x.User.UserName,
                                                                                        UserProfilePhoto = x.User.ProfilePhotoPath,
                                                                                        CommentText = x.CommentText,
                                                                                        Time = x.CreateTime
                                                                                    }).OrderBy(x => x.Time).ToList(),
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
