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

        /// <summary>
        ///  lamdba 表达式转换成IFieldPredicate 待完善
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TPrimaryKey"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IPredicate ExecuteFilter<TEntity, TPrimaryKey>(Expression<Func<TEntity, bool>> predicate) where TEntity : class, IEntity<TPrimaryKey>
        {
            IFieldPredicate fieldPredicate = null;
            //fieldPredicate = Predicates.Field<TEntity>(predicate, predicate.Type, predicate.Body.Type);
            return fieldPredicate;
        }

        public PredicateGroup ExecuteFilter<TEntity, TPrimaryKey>() where TEntity : class, IEntity<TPrimaryKey>
        {
            return null;
        }
    }
}
