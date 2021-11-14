using Candle.Business.Abstract;
using Candle.Common.Result;
using Candle.Model.DTOs.ResponseDto.ProfileResponseDto;

namespace Candle.Business.Service
{
    public class ProfileService : IProfileService
    {
        private readonly IPostService postService;
        private readonly IFollowerService followerService;
        private readonly IUserService userService;

        public ProfileService()
        {
            postService = new PostService();
            followerService = new FollowerService();
            userService = new UserService();
        }

        public IDataResult<GetProfileInfoDto>  GetProfileCount(string userName)
        {
            GetProfileInfoDto getProfileCount = new()
            {
                Post = postService.GetPostCount(userName),
                Follower = followerService.GetFollowerCount(userName),
                Following = followerService.GetFollowingCount(userName),
                ProfilePhotoPath = userService.GetProfilePhotoPath(userName)
            };

            return new SuccessDataResult<GetProfileInfoDto>(getProfileCount);
        }
    }
}
