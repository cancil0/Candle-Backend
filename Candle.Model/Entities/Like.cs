using Candle.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candle.Model.Entities
{
    public class Like : BaseEntity
    {
        /// <summary>
        /// Used for Post Isliked or Not
        /// </summary>
        public bool IsLiked { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid PostId { get; set; }
        public Post Post { get; set; }
    }
}
