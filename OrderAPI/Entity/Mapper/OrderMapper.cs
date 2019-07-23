using DapperExtensions.Mapper;

namespace OrderAPI.Entity.Mapper
{
    /// <summary>
    /// 
    /// </summary>
    public class OrderMapper : ClassMapper<Order>
    {
        /// <summary>
        /// 
        /// </summary>
        public OrderMapper()
        {
            Table("Order");
            Map(x => x.Id).Key(KeyType.Identity);
            AutoMap();
        }
    }
}
