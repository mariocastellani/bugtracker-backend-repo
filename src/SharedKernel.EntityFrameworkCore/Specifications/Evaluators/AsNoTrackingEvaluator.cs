namespace SharedKernel.EntityFrameworkCore.Specifications
{
    public class AsNoTrackingEvaluator : IEvaluator
    {
        #region Singleton

        public static AsNoTrackingEvaluator Instance { get; }

        static AsNoTrackingEvaluator()
        {
            Instance = new AsNoTrackingEvaluator();
        }

        private AsNoTrackingEvaluator() 
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
            if (specification.AsNoTracking)
                query = query.AsNoTracking();

            return query;
        }
    }
}