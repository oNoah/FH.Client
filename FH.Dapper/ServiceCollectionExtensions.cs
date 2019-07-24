using DapperExtensions.Mapper;
using DapperExtensions.Sql;
using FH.Dapper;
using FH.Dapper.Filters.Query;
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
        public static IServiceCollection AddDapper(this IServiceCollection services, string connectionString)
        {
            services.AddSingleton(DbConnectInstance(connectionString));

            DbConnection DbConnectInstance(string conn)
            {
                return new SqlConnection(conn);
            }
            services.AddSingleton<SessionServicePriver>();
            services.AddSingleton<IDapperQueryFilterExecuter, DapperQueryFilterExecuter>();
            services.AddSingleton(typeof(IDapperRepository<,>), typeof(DapperRepositoryBase<,>));
            services.AddSingleton(typeof(IDapperRepository<>), typeof(DapperRepositoryBase<>));

            //DapperExtensions.DapperExtensions.SqlDialect = new SqliteDialect();
            //DapperExtensions.DapperExtensions.SetMappingAssemblies(new List<Assembly> { Assembly.GetEntryAssembly() });
            return services;
        }
    }
}
