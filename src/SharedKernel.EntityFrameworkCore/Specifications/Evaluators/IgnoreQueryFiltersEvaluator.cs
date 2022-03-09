namespace SharedKernel.EntityFrameworkCore.Specifications
{
    /// <summary>
    /// This evaluator applies EF Core's IgnoreQueryFilters feature to a given query
    /// See: https://docs.microsoft.com/en-us/ef/core/querying/filters
    /// </summary>
    public class IgnoreQueryFiltersEvaluator : IEvaluator
    {
        #region Singleton

        public static IgnoreQueryFiltersEvaluator Instance { get; }

        static IgnoreQueryFiltersEvaluator()
        {
            Instance = new IgnoreQueryFiltersEvaluator();
        }

        private IgnoreQueryFiltersEvaluator() 
        { 
        }

        #endregion

        public bool IsCriteriaEvaluator
        {
            get { return true; }
        }

        public IQueryable<TEntity> GetQuery<TEntity>(IQueryable<TEntity> query, ISpecification<TEntity> specification) 
            where TEntity : class, IEntity
        {
            if (specification.IgnoreQueryFilters)
                query = query.IgnoreQueryFilters();

            return query;
        }
    }
}