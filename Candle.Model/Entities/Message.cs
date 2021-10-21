using Candle.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candle.Model.Entities
{
    public class Message : BaseEntity
    {
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public short MessageStatus { get; set; }
        public string MessageText { get; set; }
    }
}
