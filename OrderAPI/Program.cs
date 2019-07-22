using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace OrderAPI
{
    /// <summary>
    /// 
    /// </summary>
    public class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {

            var host = CreateWebHostBuilder(args).Build();

            host.Run();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            //var configuration = new ConfigurationBuilder().SetBasePath(Environment.CurrentDirectory)
            //                            .AddJsonFile("host.json")
            //                            .Build();

            return WebHost.CreateDefaultBuilder(args)
            //.UseConfiguration(configuration)
            .UseStartup<Startup>();
        }
    }
}