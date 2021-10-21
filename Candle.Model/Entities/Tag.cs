using Candle.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candle.Model.Entities
{
    public class Tag : BaseEntity
    {
        /// <summary>
        /// Tags at the Post
        /// </summary>
        public string TagName { get; set; }
        public Guid PostId { get; set; }
        public Post Post { get; set; }
    }
}
