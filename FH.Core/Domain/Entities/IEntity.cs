using System;
using System.Collections.Generic;
using System.Text;

namespace FH.Core.Domain.Entities
{
    /// <summary>
    /// 系统中的所有实体有主键的都必须实现此接口。
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface IEntity<TKey>
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        TKey Id { get; set; }
    }
}
