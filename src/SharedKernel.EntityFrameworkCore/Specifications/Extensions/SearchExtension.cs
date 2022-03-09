using System.Data;
using System.Linq.Expressions;
using System.Reflection;

namespace SharedKernel.EntityFrameworkCore.Specifications
{
    public static class SearchExtension
    {
        private static readonly MethodInfo _likeMethodInfo;
        private static readonly MemberExpression _functions;

        static SearchExtension()
        {
            _likeMethodInfo = typeof(DbFunctionsExtensions)
                .GetMethod(nameof(DbFunctionsExtensions.Like), new Type[] { typeof(DbFunctions), typeof(string), typeof(string) })
                    ?? throw new TargetException("The EF.Functions.Like not found.");

            _functions = Expression.Property(null, typeof(EF).GetProperty(nameof(EF.Functions))
                ?? throw new TargetException("The EF.Functions not found."));
        }

        /// <summary>
        /// Filters <paramref name="source"/> by applying an 'SQL LIKE' operation to it.
        /// </summary>
        /// <typeparam name="TEntity">The type being queried against.</typeparam>
        /// <param name="source">The sequence of <typeparamref name="TEntity"/></param>
        /// <param name="criterias">
        /// <list type="bullet">
        ///     <item>Selector, the property to apply the SQL LIKE against.</item>
        ///     <item>SearchTerm, the value to use for the SQL LIKE.</item>
        /// </list>
        /// </param>
        /// <returns></returns>
        public static IQueryable<TEntity> Search<TEntity>(this IQueryable<TEntity> source, IEnumerable<SearchExpressionInfo<TEntity>> criterias)
        {
            Expression expr = null;
            var parameter = Expression.Parameter(typeof(TEntity), "x");

            foreach (var criteria in criterias)
            {
                if (string.IsNullOrEmpty(criteria.SearchTerm))
                    continue;

                var propertySelector = ParameterReplacerVisitor.Replace(criteria.Selector, criteria.Selector.Parameters[0], parameter) as LambdaExpression
                    ?? throw new InvalidExpressionException();

                var likeExpression = Expression
                    .Call(null, _likeMethodInfo, _functions, propertySelector.Body, Expression.Constant(criteria.SearchTerm));

                expr = expr != null 
                    ? Expression.OrElse(expr, likeExpression)
                    : likeExpression;
            }

            return expr != null
                ? source.Where(Expression.Lambda<Func<TEntity, bool>>(expr, parameter))
                : source;
        }
    }
}