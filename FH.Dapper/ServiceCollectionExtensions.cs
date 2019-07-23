using DapperExtensions.Mapper;
using DapperExtensions.Sql;
using FH.Dapper;
using FH.Dapper.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Reflection;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DapperServiceExtensions
    {
        public static IServiceCollection AddDapper(this IServiceCollection services, string conn)
        {
            services.AddSingleton(DbConnectInstance(conn));

            DbConnection DbConnectInstance(string connectionString)
            {
                var connection = new SqlConnection(connectionString);
                return connection;
            }
            services.AddSingleton<SessionServicePriver>();
            services.AddSingleton(typeof(IDapperRepository<,>), typeof(DapperRepositoryBase<,>));
            services.AddSingleton(typeof(IDapperRepository<>), typeof(DapperRepositoryBase<,>));

            DapperExtensions.DapperExtensions.SqlDialect = new SqliteDialect();
            var ass = Assembly.GetEntryAssembly();
            
            DapperExtensions.DapperExtensions.SetMappingAssemblies(new List<Assembly> { ass });
            return services;
        }
    }
}
