using Candle.Business.Abstract;
using Candle.Common.Result;
using Candle.Model.DTOs.ResponseDto.ProfileResponseDto;

namespace Candle.Business.Service
{
    public class ProfileService : IProfileService
    {
        private readonly IPostService postService;
        private readonly IFollowerService followerService;

        public ProfileService()
        {
            postService = new PostService();
            followerService = new FollowerService();
        }

        public IDataResult<GetProfileCountDto>  GetProfileCount(string userName)
        {
            GetProfileCountDto getProfileCount = new()
            {
                Post = postService.GetPostCount(userName),
                Follower = followerService.GetFollowerCount(userName),
                Following = followerService.GetFollowingCount(userName),
            };

            return new SuccessDataResult<GetProfileCountDto>(getProfileCount);
        }
    }
}
