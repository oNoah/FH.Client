using DapperExtensions.Mapper;

namespace OrderAPI.Entity.Mapper
{
    /// <summary>
    /// 
    /// </summary>
    public class OrderMapper : ClassMapper<Order>
    {
        /// <summary>
        /// Mapper 不是必须配置，配置后以Mapper内容为准
        /// </summary>
        public OrderMapper()
        {
            Table("Order");
            Map(x => x.Id).Key(KeyType.Identity);
            AutoMap();
        }
    }
}
