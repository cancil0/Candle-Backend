using System;
using System.Collections.Generic;

namespace Candle.Model.Entities
{
    public class Follower
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid FollowerId { get; set; }
        public virtual User User { get; set; }
        public virtual User UserFollower { get; set; }
    }
}
