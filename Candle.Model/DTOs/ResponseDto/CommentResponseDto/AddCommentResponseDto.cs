using System;

namespace Candle.Model.DTOs.ResponseDto.CommentResponseDto
{
    public class AddCommentResponseDto
    {
        public Guid CommentId { get; set; }

        public string UserProfilePhoto { get; set; }
    }
}
