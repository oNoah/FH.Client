using FH.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FH.Dapper.Repositories
{
    public interface IDapperRepository<TEntity> : IDapperRepository<TEntity, int> 
        where TEntity : class,
        IEntity<int>
    {
    }
}
