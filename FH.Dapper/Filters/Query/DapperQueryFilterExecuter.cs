using DapperExtensions;
using FH.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace FH.Dapper.Filters.Query
{
    public class DapperQueryFilterExecuter : IDapperQueryFilterExecuter
    {
        public static readonly DapperQueryFilterExecuter Instance = new DapperQueryFilterExecuter();

        public IPredicate ExecuteFilter<TEntity, TPrimaryKey>(Expression<Func<TEntity, bool>> predicate) where TEntity : class, IEntity<TPrimaryKey>
        {
            return null;
        }

        public PredicateGroup ExecuteFilter<TEntity, TPrimaryKey>() where TEntity : class, IEntity<TPrimaryKey>
        {
            return null;
        }
    }
}
