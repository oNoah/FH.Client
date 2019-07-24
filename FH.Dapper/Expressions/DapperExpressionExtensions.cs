using DapperExtensions;
using FH.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace FH.Dapper.Expressions
{
    internal static class DapperExpressionExtensions
    {
        /// <summary>
        /// Lamdba表达式转IPredicate
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TPrimaryKey"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static IPredicate ToPredicateGroup<TEntity, TPrimaryKey>(this Expression<Func<TEntity, bool>> expression) where TEntity : class, IEntity<TPrimaryKey>
        {
            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            var dev = new DapperExpressionVisitor<TEntity, TPrimaryKey>();
            IPredicate pg = dev.Process(expression);

            return pg;
        }
    }
}
