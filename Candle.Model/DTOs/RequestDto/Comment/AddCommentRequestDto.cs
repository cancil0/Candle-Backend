using System;

namespace Candle.Model.DTOs.RequestDto.Comment
{
    public class AddCommentRequestDto
    {
        public Guid UserId { get; set; }
        public Guid PostId { get; set; }
        public Guid? ParentCommentId { get; set; }
        public string CommentText { get; set; }
    }
}
