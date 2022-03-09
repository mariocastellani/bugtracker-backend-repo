namespace SharedKernel.EntityFrameworkCore.Specifications
{
    public class AsNoTrackingWithIdentityResolutionEvaluator : IEvaluator
    {
        #region Singleton

        public static AsNoTrackingWithIdentityResolutionEvaluator Instance { get; }

        static AsNoTrackingWithIdentityResolutionEvaluator()
        {
            Instance = new AsNoTrackingWithIdentityResolutionEvaluator();
        }

        private AsNoTrackingWithIdentityResolutionEvaluator() 
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
            if (specification.AsNoTrackingWithIdentityResolution)
                query = query.AsNoTrackingWithIdentityResolution();

            return query;
        }
    }
}