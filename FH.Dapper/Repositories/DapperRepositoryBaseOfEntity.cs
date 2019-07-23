using FH.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FH.Dapper.Repositories
{
    public class DapperRepositoryBase<TEntity> : DapperRepositoryBase<TEntity, int>
        where TEntity : class,
        IEntity<int>
    {
        public DapperRepositoryBase(SessionServicePriver sessionServicePriver)
            : base(sessionServicePriver)
        {

        }
    }
}
