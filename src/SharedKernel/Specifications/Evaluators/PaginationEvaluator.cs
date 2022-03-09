namespace SharedKernel.Specifications
{
    public class PaginationEvaluator : IEvaluator, IInMemoryEvaluator
    {
        #region Singleton

        public static PaginationEvaluator Instance { get; }

        static PaginationEvaluator()
        {
            Instance = new PaginationEvaluator();
        }

        private PaginationEvaluator() 
        { 
        }

        #endregion

        public bool IsCriteriaEvaluator
        {
            get { return false; }
        }

        public IQueryable<TEntity> GetQuery<TEntity>(IQueryable<TEntity> query, ISpecification<TEntity> specification) 
            where TEntity : class, IEntity
        {
            // If skip is 0, avoid adding to the IQueryable
            // It will generate more optimized SQL that way
            if (specification.Skip != null && specification.Skip != 0)
                query = query.Skip(specification.Skip.Value);

            if (specification.Take != null)
                query = query.Take(specification.Take.Value);

            return query;
        }

        public IEnumerable<TEntity> Evaluate<TEntity>(IEnumerable<TEntity> query, ISpecification<TEntity> specification)
            where TEntity : IEntity
        {
            if (specification.Skip != null && specification.Skip != 0)
                query = query.Skip(specification.Skip.Value);

            if (specification.Take != null)
                query = query.Take(specification.Take.Value);

            return query;
        }
    }
}