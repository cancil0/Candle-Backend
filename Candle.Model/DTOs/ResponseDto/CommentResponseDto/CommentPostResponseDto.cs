using System;

namespace Candle.Model.DTOs.CommentResponseDto.CommentResponseDto
{
    public class CommentPostResponseDto
    {
        public string CommentText { get; set; }
        public string UserName { get; set; }
        public Guid CommentId { get; set; }
        public Guid? ParentCommentId { get; set; }
        public DateTime Time { get; set; }
        public string UserProfilePhoto { get; set; }
    }
}
