using DapperExtensions;
using FH.Core.Domain.Entities;
using FH.Dapper.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
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
            IPredicate pg = predicate.ToPredicateGroup<TEntity, TPrimaryKey>();
            return pg;
        }

        /// <summary>
        /// 转换Predicate
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TPrimaryKey"></typeparam>
        /// <returns></returns>
        public PredicateGroup ExecuteFilter<TEntity, TPrimaryKey>() where TEntity : class, IEntity<TPrimaryKey>
        {
            var groups = new PredicateGroup
            {
                Operator = GroupOperator.And,
                Predicates = new List<IPredicate>()
            };
            IFieldPredicate predicate = ExecuteFilterToPredicate<TEntity, TPrimaryKey>();
            if (predicate != null)
            {
                groups.Predicates.Add(predicate);
            }
            return groups;
        }

        public bool IsDeleted => false;

        public bool IsEnabled => true;

        public IFieldPredicate ExecuteFilterToPredicate<TEntity, TPrimaryKey>() where TEntity : class, IEntity<TPrimaryKey>
        {
            IFieldPredicate predicate = null;
            if (IsFilterable<TEntity, TPrimaryKey>())
            {
                predicate = Predicates.Field<TEntity>(f => (f as ISoftDelete).IsDeleted, Operator.Eq, IsDeleted);
            }

            return predicate;
        }

        private bool IsFilterable<TEntity, TPrimaryKey>() where TEntity : class, IEntity<TPrimaryKey>
        {
            return typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity)) && IsEnabled;
        }
    }
}
