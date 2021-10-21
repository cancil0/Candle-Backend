using Candle.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candle.Model.Entities
{
    public class Post : BaseEntity
    {
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public string Content { get; set; }
        public virtual ICollection<Media> Medias { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Like> Likes { get; set; }

    }
}
