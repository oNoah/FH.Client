using System;
using System.Collections.Generic;
using System.Text;

namespace FH.Core.Domain.Entities
{
    /// <summary>
    /// 软删除
    /// </summary>
    public interface ISoftDelete
    {
        /// <summary>
        /// 标记实体被删除
        /// </summary>
        bool IsDeleted { get; set; }
    }
}
