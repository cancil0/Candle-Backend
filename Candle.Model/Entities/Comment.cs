using Candle.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candle.Model.Entities
{
    public class Comment : BaseEntity
    {
        /// <summary>
        /// Comment at the Post
        /// </summary>
        public string CommentText { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid PostId { get; set; }
        public Post Post { get; set; }
        public Guid? ParentCommentId { get; set; }
        public Comment ParentComment { get; set; }
        public IList<Comment> Replies { get; protected set; } = new List<Comment>();
    }
}
