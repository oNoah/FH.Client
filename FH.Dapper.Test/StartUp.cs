using FH.Core.Domain.Entities;
using FH.Dapper.Repositories;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Xunit;

namespace FH.Dapper.Test
{
    public class StartUp
    {
        [Fact]
        public void Init()
        {
            var host = WebHost.CreateDefaultBuilder()
                .ConfigureServices(services =>
                {
                    services.AddDapper("Data Source=172.16.88.233;Initial Catalog=Test_Db;User Id=sa;Password=Hy@123456;");
                })
                .Configure(app =>
                {

                })
                ;
        }
        public class Order : IEntity<int>
        {
            public int Id { get; set; }

            public string Name { get; set; }
        }

        public class Dapper_Test
        {
            public IDapperRepository<Order> DapperRepository;


            public Dapper_Test(IDapperRepository<Order> dapperRepository)
            {
                DapperRepository = dapperRepository;
            }

            [Fact]
            public async Task TestConnection()
            {
                var order = await DapperRepository.SingleAsync(1);
            }
        }
    }
}
