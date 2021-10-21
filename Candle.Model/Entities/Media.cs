using Candle.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candle.Model.Entities
{
    public class Media : BaseEntity
    {
        public string Caption { get; set; }

        public int FileSize { get; set; }

        public string FileName { get; set; }

        public short MediaType { get; set; }

        public Guid PostId { get; set; }
        public virtual Post Post { get; set; }
    }
}
