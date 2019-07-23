using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using DapperExtensions;
using FH.Core.Domain.Entities;
using FH.Dapper.Repositories;
using Microsoft.AspNetCore.Mvc;
using OrderAPI.Entity;

namespace OrderAPI.Controllers
{
    /// <summary>
    /// 测试接口
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
    
        private IServiceProvider ServiceProvider;
        private readonly IDapperRepository<Order, int> _orderRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderRepository"></param>
        /// <param name="serviceProvider"></param>
        public ValuesController(
            IDapperRepository<Order, int> orderRepository,
            IServiceProvider serviceProvider)
        {
                _orderRepository = orderRepository;
        }

        /// <summary>
        /// GET api/values
        /// </summary>
        /// <returns></returns>
        [HttpGet("Get")]
        public ActionResult<IEnumerable<string>> Get()
        {
            var test = _orderRepository.GetAll();

            using (SqlConnection cn = new SqlConnection("Data Source=172.16.88.233;Initial Catalog=Test_Db;User Id=sa;Password=Hy@123456;"))
            {
                cn.Open();
                var predicate = Predicates.Field<Order>(f => f.Id, Operator.Eq, true);
                IEnumerable<Order> list = cn.GetList<Order>(predicate);

                var pr = Predicates.Field<Order>(x => x.Name, Operator.Like, "2");
                cn.Close();
            }

            //var test =  _orderRepository.Single(1);
            return new string[] { "value1", "value2" };
        }

        /// <summary>
        /// GET api/values/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

    }
}
