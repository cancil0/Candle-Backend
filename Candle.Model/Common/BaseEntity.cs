using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candle.Model.Common
{
    /// <summary>
    /// Base Model
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// Unique Value
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Shows Who Created the Data
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Shows Who Updated the Data
        /// </summary>
        public string UpdatedBy { get; set; }

        /// <summary>
        /// Shows That Data Is Active or Not
        /// 0 for Not Active
        /// 1 for Active
        /// </summary>
        public short IsActive { get; set; }

        /// <summary>
        /// Shows When Created the Data
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Shows When Updated the Data
        /// </summary>
        public DateTime? UpdateTime { get; set; }
    }
}
