namespace SharedKernel.EntityFrameworkCore.Specifications
{
    public class AsSplitQueryEvaluator : IEvaluator
    {
        #region Singleton

        public static AsSplitQueryEvaluator Instance { get; }

        static AsSplitQueryEvaluator()
        {
            Instance = new AsSplitQueryEvaluator();
        }

        private AsSplitQueryEvaluator() 
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
            if (specification.AsSplitQuery)
                query = query.AsSplitQuery();

            return query;
        }
    }
}