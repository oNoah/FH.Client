using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace FH.Dapper
{
    public class SessionServicePriver
    {
        private readonly IServiceProvider ServiceProvider;

        public SessionServicePriver(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        public DbTransaction GetTransaction()
        {
            var tran = GetConnection().BeginTransaction();
            return tran;
        }

        private static DbConnection DbConnection = null;

        public DbConnection GetConnection()
        {
            if (DbConnection == null)
                DbConnection = ServiceProvider.GetService(typeof(DbConnection)) as DbConnection;
            if (DbConnection.State == ConnectionState.Closed)
                DbConnection.Open();
            return DbConnection;
        }
    }
}
