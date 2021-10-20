using System;
using System.Linq;
using System.Linq.Expressions;

namespace Persistence.Ef.Extensions
{
    internal static class RepositoryExtensions
    {
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string fieldName, bool isDescending = true)
        {
            if (string.IsNullOrEmpty(fieldName))
                return source;

            ParameterExpression parameter = Expression.Parameter(source.ElementType, "");

            MemberExpression property = Expression.Property(parameter, fieldName);
            LambdaExpression lambda = Expression.Lambda(property, parameter);

            string methodName = isDescending ? "OrderByDescending" : "OrderBy";

            Expression methodCallExpression = Expression.Call(typeof(Queryable), methodName,
                                  new Type[] { source.ElementType, property.Type },
                                  source.Expression, Expression.Quote(lambda));

            return source.Provider.CreateQuery<T>(methodCallExpression);
        }
    }
}
