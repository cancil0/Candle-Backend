using System;

namespace Candle.Model.DTOs.ResponseDto.FollowerResponseDto
{
    public class GetFollowerResponseDto
    {
        public Guid Id { get; set; }
        public Guid FollowerId { get; set; }
        public string FollowerName { get; set; }
    }
}
