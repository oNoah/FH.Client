﻿using DapperExtensions;
using FH.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace FH.Dapper.Filters.Query
{
    public class NullDapperQueryFilterExecuter : IDapperQueryFilterExecuter
    {
        public static readonly NullDapperQueryFilterExecuter Instance = new NullDapperQueryFilterExecuter();

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
