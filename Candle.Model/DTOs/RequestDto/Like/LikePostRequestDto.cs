using System;

namespace Candle.Model.DTOs.RequestDto.Like
{
    public class LikePostRequestDto
    {
        public Guid UserId { get; set; }

        public Guid PostId { get; set; }
    }
}
