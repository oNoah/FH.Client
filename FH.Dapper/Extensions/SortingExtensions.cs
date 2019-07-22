using DapperExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace FH.Dapper.Extensions
{
    internal static class SortingExtensions
    {
        public static List<ISort> ToSortable<T>(this Expression<Func<T, object>>[] sortingExpression, bool ascending = true)
        {
            if (sortingExpression == null || ( sortingExpression != null && sortingExpression.Length  == 0))
            {
                var parameterName = nameof(sortingExpression);
                throw new ArgumentException(parameterName + " can not be null or empty!", parameterName);
            }

            var sortList = new List<ISort>();
            sortingExpression.ToList().ForEach(sortExpression =>
            {
                MemberInfo sortProperty = ReflectionHelper.GetProperty(sortExpression);
                sortList.Add(new Sort { Ascending = ascending, PropertyName = sortProperty.Name });
            });

            return sortList;
        }
    }
}
