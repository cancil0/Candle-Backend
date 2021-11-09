using System;

namespace Candle.Model.DTOs.RequestDto.Follower
{
    public class FollowerRequestDto
    {
        public Guid UserId { get; set; }
        public Guid FollowerId { get; set; }
    }
}
