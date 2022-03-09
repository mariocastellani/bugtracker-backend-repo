using System.Linq.Expressions;

namespace SharedKernel.Specifications
{
    public static class SpecificationBuilderExtensions
    {
        #region Where

        /// <summary>
        /// Specify a predicate that will be applied to the query.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="builder"></param>
        /// <param name="criteria"></param>
        public static ISpecificationBuilder<TEntity> Where<TEntity>(
            this ISpecificationBuilder<TEntity> builder, Expression<Func<TEntity, bool>> criteria)
            where TEntity : IEntity
        {
            return Where(builder, criteria, true);
        }

        /// <summary>
        /// Specify a predicate that will be applied to the query
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="builder"></param>
        /// <param name="criteria"></param>
        /// <param name="condition">If false, the criteria won't be added.</param>
        public static ISpecificationBuilder<TEntity> Where<TEntity>(
            this ISpecificationBuilder<TEntity> builder, Expression<Func<TEntity, bool>> criteria, bool condition)
            where TEntity : IEntity
        {
            if (condition)
                ((List<WhereExpressionInfo<TEntity>>)builder.Specification.WhereExpressions)
                    .Add(new WhereExpressionInfo<TEntity>(criteria));

            return builder;
        }

        #endregion

        #region OrderBy

        /// <summary>
        /// Specify the query result will be ordered by <paramref name="orderExpression"/> in an ascending order
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="builder"></param>
        /// <param name="orderExpression"></param>
        public static IOrderedSpecificationBuilder<TEntity> OrderBy<TEntity>(
            this ISpecificationBuilder<TEntity> builder, Expression<Func<TEntity, object>> orderExpression)
            where TEntity : IEntity
        {
            return OrderBy(builder, orderExpression, true);
        }

        /// <summary>
        /// Specify the query result will be ordered by <paramref name="orderExpression"/> in an ascending order
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="builder"></param>
        /// <param name="orderExpression"></param>
        /// <param name="condition">If false, the expression won't be added. The whole Order chain will be discarded.</param>
        public static IOrderedSpecificationBuilder<TEntity> OrderBy<TEntity>(
            this ISpecificationBuilder<TEntity> builder, Expression<Func<TEntity, object>> orderExpression, bool condition)
            where TEntity : IEntity
        {
            if (condition)
                ((List<OrderExpressionInfo<TEntity>>)builder.Specification.OrderExpressions)
                    .Add(new OrderExpressionInfo<TEntity>(orderExpression, OrderType.OrderBy));

            return new OrderedSpecificationBuilder<TEntity>(builder.Specification, !condition);
        }

        /// <summary>
        /// Specify the query result will be ordered by <paramref name="orderExpression"/> in a descending order
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="builder"></param>
        /// <param name="orderExpression"></param>
        public static IOrderedSpecificationBuilder<TEntity> OrderByDescending<TEntity>(
            this ISpecificationBuilder<TEntity> builder, Expression<Func<TEntity, object>> orderExpression)
            where TEntity : IEntity
        {
            return OrderByDescending(builder, orderExpression, true);
        }

        /// <summary>
        /// Specify the query result will be ordered by <paramref name="orderExpression"/> in a descending order
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="builder"></param>
        /// <param name="orderExpression"></param>
        /// <param name="condition">If false, the expression won't be added. The whole Order chain will be discarded.</param>
        public static IOrderedSpecificationBuilder<TEntity> OrderByDescending<TEntity>(
            this ISpecificationBuilder<TEntity> builder, Expression<Func<TEntity, object?>> orderExpression, bool condition)
            where TEntity : IEntity
        {
            if (condition)
                ((List<OrderExpressionInfo<TEntity>>)builder.Specification.OrderExpressions)
                    .Add(new OrderExpressionInfo<TEntity>(orderExpression, OrderType.OrderByDescending));

            return new OrderedSpecificationBuilder<TEntity>(builder.Specification, !condition);
        }

        #endregion

        #region ThenBy (pos OrderBy)

        public static IOrderedSpecificationBuilder<TEntity> ThenBy<TEntity>(
            this IOrderedSpecificationBuilder<TEntity> orderedBuilder,
            Expression<Func<TEntity, object>> orderExpression)
            where TEntity : IEntity
        {
            return ThenBy(orderedBuilder, orderExpression, true);
        }

        public static IOrderedSpecificationBuilder<TEntity> ThenBy<TEntity>(
            this IOrderedSpecificationBuilder<TEntity> orderedBuilder,
            Expression<Func<TEntity, object>> orderExpression,
            bool condition)
            where TEntity : IEntity
        {
            if (condition && !orderedBuilder.IsChainDiscarded)
                ((List<OrderExpressionInfo<TEntity>>)orderedBuilder.Specification.OrderExpressions)
                    .Add(new OrderExpressionInfo<TEntity>(orderExpression, OrderType.ThenBy));
            else
                orderedBuilder.IsChainDiscarded = true;

            return orderedBuilder;
        }

        public static IOrderedSpecificationBuilder<TEntity> ThenByDescending<TEntity>(
            this IOrderedSpecificationBuilder<TEntity> orderedBuilder,
            Expression<Func<TEntity, object>> orderExpression)
            where TEntity : IEntity
        {
            return ThenByDescending(orderedBuilder, orderExpression, true);
        }

        public static IOrderedSpecificationBuilder<TEntity> ThenByDescending<TEntity>(
            this IOrderedSpecificationBuilder<TEntity> orderedBuilder,
            Expression<Func<TEntity, object>> orderExpression,
            bool condition)
            where TEntity : IEntity
        {
            if (condition && !orderedBuilder.IsChainDiscarded)
                ((List<OrderExpressionInfo<TEntity>>)orderedBuilder.Specification.OrderExpressions)
                    .Add(new OrderExpressionInfo<TEntity>(orderExpression, OrderType.ThenByDescending));
            else
                orderedBuilder.IsChainDiscarded = true;

            return orderedBuilder;
        }

        #endregion

        #region Include

        /// <summary>
        /// Specify an include expression.
        /// This information is utilized to build Include function in the query, which ORM tools like Entity Framework use
        /// to include related entities (via navigation properties) in the query result.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="builder"></param>
        /// <param name="includeExpression"></param>
        public static IIncludableSpecificationBuilder<TEntity, TProperty> Include<TEntity, TProperty>(
            this ISpecificationBuilder<TEntity> builder, Expression<Func<TEntity, TProperty>> includeExpression)
            where TEntity : IEntity
        {
            return Include(builder, includeExpression, true);
        }

        /// <summary>
        /// Specify an include expression.
        /// This information is utilized to build Include function in the query, which ORM tools like Entity Framework use
        /// to include related entities (via navigation properties) in the query result.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="builder"></param>
        /// <param name="includeExpression"></param>
        /// <param name="condition">If false, the expression won't be added. The whole Include chain will be discarded.</param>
        public static IIncludableSpecificationBuilder<TEntity, TProperty> Include<TEntity, TProperty>(
            this ISpecificationBuilder<TEntity> builder, Expression<Func<TEntity, TProperty>> includeExpression, bool condition) 
            where TEntity : IEntity
        {
            if (condition)
                ((List<IncludeExpressionInfo>)builder.Specification.IncludeExpressions)
                    .Add(new IncludeExpressionInfo(includeExpression, typeof(TEntity), typeof(TProperty)));

            return new IncludableSpecificationBuilder<TEntity, TProperty>(builder.Specification, !condition);
        }

        /// <summary>
        /// Specify a collection of navigation properties, as strings, to include in the query.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="builder"></param>
        /// <param name="includeString"></param>
        public static ISpecificationBuilder<TEntity> Include<TEntity>(
            this ISpecificationBuilder<TEntity> builder, string includeString)
            where TEntity : IEntity
        {
            return Include(builder, includeString, true);
        }

        /// <summary>
        /// Specify a collection of navigation properties, as strings, to include in the query.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="builder"></param>
        /// <param name="includeString"></param>
        /// <param name="condition">If false, the include expression won't be added.</param>
        public static ISpecificationBuilder<TEntity> Include<TEntity>(
            this ISpecificationBuilder<TEntity> builder, string includeString, bool condition) 
            where TEntity : IEntity
        {
            if (condition)
                ((List<string>)builder.Specification.IncludeStrings).Add(includeString);

            return builder;
        }

        #endregion

        #region ThenInclude (pos Include)

        public static IIncludableSpecificationBuilder<TEntity, TProperty> ThenInclude<TEntity, TPreviousProperty, TProperty>(
            this IIncludableSpecificationBuilder<TEntity, TPreviousProperty> previousBuilder,
            Expression<Func<TPreviousProperty, TProperty>> thenIncludeExpression)
            where TEntity : IEntity
        {
            return ThenInclude(previousBuilder, thenIncludeExpression, true);
        }

        public static IIncludableSpecificationBuilder<TEntity, TProperty> ThenInclude<TEntity, TPreviousProperty, TProperty>(
            this IIncludableSpecificationBuilder<TEntity, TPreviousProperty> previousBuilder,
            Expression<Func<TPreviousProperty, TProperty>> thenIncludeExpression,
            bool condition)
            where TEntity : IEntity
        {
            if (condition && !previousBuilder.IsChainDiscarded)
                ((List<IncludeExpressionInfo>)previousBuilder.Specification.IncludeExpressions)
                    .Add(new IncludeExpressionInfo(thenIncludeExpression, typeof(TEntity), typeof(TProperty), typeof(TPreviousProperty)));

            return new IncludableSpecificationBuilder<TEntity, TProperty>(previousBuilder.Specification, !condition || previousBuilder.IsChainDiscarded);
        }

        public static IIncludableSpecificationBuilder<TEntity, TProperty> ThenInclude<TEntity, TPreviousProperty, TProperty>(
            this IIncludableSpecificationBuilder<TEntity, IEnumerable<TPreviousProperty>> previousBuilder,
            Expression<Func<TPreviousProperty, TProperty>> thenIncludeExpression)
            where TEntity : IEntity
        {
            return ThenInclude(previousBuilder, thenIncludeExpression, true);
        }

        public static IIncludableSpecificationBuilder<TEntity, TProperty> ThenInclude<TEntity, TPreviousProperty, TProperty>(
            this IIncludableSpecificationBuilder<TEntity, IEnumerable<TPreviousProperty>> previousBuilder,
            Expression<Func<TPreviousProperty, TProperty>> thenIncludeExpression,
            bool condition)
            where TEntity : IEntity
        {
            if (condition && !previousBuilder.IsChainDiscarded)
                ((List<IncludeExpressionInfo>)previousBuilder.Specification.IncludeExpressions)
                    .Add(new IncludeExpressionInfo(thenIncludeExpression, typeof(TEntity), typeof(TProperty), typeof(IEnumerable<TPreviousProperty>)));

            return new IncludableSpecificationBuilder<TEntity, TProperty>(previousBuilder.Specification, !condition || previousBuilder.IsChainDiscarded);
        }

        #endregion

        #region Search (Like)

        /// <summary>
        /// Specify a 'SQL LIKE' operations for search purposes
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="builder"></param>
        /// <param name="selector">the property to apply the SQL LIKE against</param>
        /// <param name="searchTerm">the value to use for the SQL LIKE</param>
        /// <param name="searchGroup">the index used to group sets of Selectors and SearchTerms together</param>
        public static ISpecificationBuilder<TEntity> Search<TEntity>(
            this ISpecificationBuilder<TEntity> builder, Expression<Func<TEntity, string>> selector,
            string searchTerm, int searchGroup = 1)
            where TEntity : IEntity
        {
            return Search(builder, selector, searchTerm, true, searchGroup);
        }

        /// <summary>
        /// Specify a 'SQL LIKE' operations for search purposes
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="builder"></param>
        /// <param name="selector">the property to apply the SQL LIKE against</param>
        /// <param name="searchTerm">the value to use for the SQL LIKE</param>
        /// <param name="condition">If false, the expression won't be added.</param>
        /// <param name="searchGroup">the index used to group sets of Selectors and SearchTerms together</param>
        public static ISpecificationBuilder<TEntity> Search<TEntity>(
            this ISpecificationBuilder<TEntity> builder, Expression<Func<TEntity, string>> selector,
            string searchTerm, bool condition, int searchGroup = 1) 
            where TEntity : IEntity
        {
            if (condition)
                ((List<SearchExpressionInfo<TEntity>>)builder.Specification.SearchCriterias)
                    .Add(new SearchExpressionInfo<TEntity>(selector, searchTerm, searchGroup));

            return builder;
        }

        #endregion

        #region Skip & Take

        /// <summary>
        /// Specify the number of elements to skip before returning the remaining elements.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="builder"></param>
        /// <param name="skip">number of elements to skip</param>
        public static ISpecificationBuilder<TEntity> Skip<TEntity>(
            this ISpecificationBuilder<TEntity> builder, int skip)
            where TEntity : IEntity
        {
            return Skip(builder, skip, true);
        }

        /// <summary>
        /// Specify the number of elements to skip before returning the remaining elements.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="builder"></param>
        /// <param name="skip">number of elements to skip</param>
        /// <param name="condition">If false, the value will be discarded.</param>
        public static ISpecificationBuilder<TEntity> Skip<TEntity>(
            this ISpecificationBuilder<TEntity> builder, int skip, bool condition)
            where TEntity : IEntity
        {
            if (condition)
            {
                if (builder.Specification.Skip != null)
                    throw new DuplicateSkipException();

                builder.Specification.Skip = skip;
            }

            return builder;
        }

        /// <summary>
        /// Specify the number of elements to return.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="take">number of elements to take</param>
        public static ISpecificationBuilder<TEntity> Take<TEntity>(
            this ISpecificationBuilder<TEntity> builder, int take)
            where TEntity : IEntity
        {
            return Take(builder, take, true);
        }

        /// <summary>
        /// Specify the number of elements to return.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="take">number of elements to take</param>
        /// <param name="condition">If false, the value will be discarded.</param>
        public static ISpecificationBuilder<TEntity> Take<TEntity>(
            this ISpecificationBuilder<TEntity> builder, int take, bool condition)
            where TEntity : IEntity
        {
            if (condition)
            {
                if (builder.Specification.Take != null)
                    throw new DuplicateTakeException();

                builder.Specification.Take = take;
            }

            return builder;
        }

        #endregion

        #region Select & Transform functions

        /// <summary>
        /// Specify a transform function to apply to the <typeparamref name="TEntity"/> element 
        /// to produce another <typeparamref name="TResult"/> element.
        /// </summary>
        public static ISpecificationBuilder<TEntity, TResult> Select<TEntity, TResult>(
            this ISpecificationBuilder<TEntity, TResult> builder, Expression<Func<TEntity, TResult>> selector)
            where TEntity : IEntity
        {
            builder.Specification.Selector = selector;

            return builder;
        }

        /// <summary>
        /// Specify a transform function to apply to the result of the query 
        /// and returns the same <typeparamref name="TEntity"/> type
        /// </summary>
        public static ISpecificationBuilder<TEntity> PostProcessingAction<TEntity>(
            this ISpecificationBuilder<TEntity> builder, Func<IEnumerable<TEntity>, IEnumerable<TEntity>> predicate)
            where TEntity : IEntity
        {
            builder.Specification.PostProcessingAction = predicate;

            return builder;
        }

        /// <summary>
        /// Specify a transform function to apply to the result of the query.
        /// and returns another <typeparamref name="TResult"/> type
        /// </summary>
        public static ISpecificationBuilder<TEntity, TResult> PostProcessingAction<TEntity, TResult>(
            this ISpecificationBuilder<TEntity, TResult> builder, Func<IEnumerable<TResult>, IEnumerable<TResult>> predicate)
            where TEntity : IEntity
        {
            builder.Specification.PostProcessingAction = predicate;

            return builder;
        }

        #endregion

        #region Caching

        /// <summary>
        /// Must be called after specifying criteria
        /// </summary>
        /// <param name="specificationName"></param>
        /// <param name="args">Any arguments used in defining the specification</param>
        public static ICacheSpecificationBuilder<TEntity> EnableCache<TEntity>(
            this ISpecificationBuilder<TEntity> builder, string specificationName, params object[] args)
            where TEntity : IEntity
        {
            return EnableCache(builder, specificationName, true, args);
        }

        /// <summary>
        /// Must be called after specifying criteria
        /// </summary>
        /// <param name="specificationName"></param>
        /// <param name="args">Any arguments used in defining the specification</param>
        /// <param name="condition">If false, the caching won't be enabled.</param>
        public static ICacheSpecificationBuilder<TEntity> EnableCache<TEntity>(
            this ISpecificationBuilder<TEntity> builder, string specificationName, bool condition, params object[] args) 
            where TEntity : IEntity
        {
            if (condition)
            {
                if (string.IsNullOrEmpty(specificationName))
                    throw new ArgumentException($"Required input {specificationName} was null or empty.", specificationName);

                builder.Specification.CacheKey = $"{specificationName}-{string.Join("-", args)}";
                builder.Specification.CacheEnabled = true;
            }

            return new CacheSpecificationBuilder<TEntity>(builder.Specification, !condition);
        }

        #endregion

        #region Others

        /// <summary>
        /// If the entity instances are modified, this will not be detected
        /// by the change tracker.
        /// </summary>
        /// <param name="builder"></param>
        public static ISpecificationBuilder<TEntity> AsNoTracking<TEntity>(
            this ISpecificationBuilder<TEntity> builder)
            where TEntity : IEntity
        {
            return AsNoTracking(builder, true);
        }

        /// <summary>
        /// If the entity instances are modified, this will not be detected
        /// by the change tracker.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="condition">If false, the setting will be discarded.</param>
        public static ISpecificationBuilder<TEntity> AsNoTracking<TEntity>(
            this ISpecificationBuilder<TEntity> builder, bool condition) 
            where TEntity : IEntity
        {
            if (condition)
                builder.Specification.AsNoTracking = true;

            return builder;
        }

        /// <summary>
        /// The generated sql query will be split into multiple SQL queries
        /// </summary>
        /// <remarks>
        /// This feature was introduced in EF Core 5.0. It only works when using Include
        /// for more info: https://docs.microsoft.com/en-us/ef/core/querying/single-split-queries
        /// </remarks>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="builder"></param>
        public static ISpecificationBuilder<TEntity> AsSplitQuery<TEntity>(
            this ISpecificationBuilder<TEntity> builder)
            where TEntity : IEntity
        {
            return AsSplitQuery(builder, true);
        }

        /// <summary>
        /// The generated sql query will be split into multiple SQL queries
        /// </summary>
        /// <remarks>
        /// This feature was introduced in EF Core 5.0. It only works when using Include
        /// for more info: https://docs.microsoft.com/en-us/ef/core/querying/single-split-queries
        /// </remarks>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="builder"></param>
        /// <param name="condition">If false, the setting will be discarded.</param>
        public static ISpecificationBuilder<TEntity> AsSplitQuery<TEntity>(
            this ISpecificationBuilder<TEntity> builder, bool condition) 
            where TEntity : IEntity
        {
            if (condition)
                builder.Specification.AsSplitQuery = true;

            return builder;
        }

        /// <summary>
        /// The query will then keep track of returned instances 
        /// (without tracking them in the normal way) 
        /// and ensure no duplicates are created in the query results
        /// </summary>
        /// <remarks>
        /// for more info: https://docs.microsoft.com/en-us/ef/core/change-tracking/identity-resolution#identity-resolution-and-queries
        /// </remarks>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="builder"></param>
        public static ISpecificationBuilder<TEntity> AsNoTrackingWithIdentityResolution<TEntity>(
            this ISpecificationBuilder<TEntity> builder) 
            where TEntity : IEntity
        {
            return AsNoTrackingWithIdentityResolution(builder, true);
        }

        /// <summary>
        /// The query will then keep track of returned instances 
        /// (without tracking them in the normal way) 
        /// and ensure no duplicates are created in the query results
        /// </summary>
        /// <remarks>
        /// for more info: https://docs.microsoft.com/en-us/ef/core/change-tracking/identity-resolution#identity-resolution-and-queries
        /// </remarks>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="builder"></param>
        /// <param name="condition">If false, the setting will be discarded.</param>
        public static ISpecificationBuilder<TEntity> AsNoTrackingWithIdentityResolution<TEntity>(
            this ISpecificationBuilder<TEntity> builder, bool condition)  
            where TEntity : IEntity
        {
            if (condition)
                builder.Specification.AsNoTrackingWithIdentityResolution = true;

            return builder;
        }

        /// <summary>
        /// The query will ignore the defined global query filters
        /// </summary>
        /// <remarks>
        /// for more info: https://docs.microsoft.com/en-us/ef/core/querying/filters
        /// </remarks>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="builder"></param>
        public static ISpecificationBuilder<TEntity> IgnoreQueryFilters<TEntity>(
            this ISpecificationBuilder<TEntity> builder)
            where TEntity : IEntity
        {
            return IgnoreQueryFilters(builder, true);
        }

        /// <summary>
        /// The query will ignore the defined global query filters
        /// </summary>
        /// <remarks>
        /// for more info: https://docs.microsoft.com/en-us/ef/core/querying/filters
        /// </remarks>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="builder"></param>
        /// <param name="condition">If false, the setting will be discarded.</param>
        public static ISpecificationBuilder<TEntity> IgnoreQueryFilters<TEntity>(
            this ISpecificationBuilder<TEntity> builder, bool condition) 
            where TEntity : IEntity
        {
            if (condition)
                builder.Specification.IgnoreQueryFilters = true;

            return builder;
        }

        #endregion
    }
}