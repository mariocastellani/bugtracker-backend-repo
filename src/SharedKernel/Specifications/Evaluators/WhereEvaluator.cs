namespace SharedKernel.Specifications
{
    public class WhereEvaluator : IEvaluator, IInMemoryEvaluator
    {
        #region Singleton

        public static WhereEvaluator Instance { get; }

        static WhereEvaluator()
        {
            Instance = new WhereEvaluator();
        }

        private WhereEvaluator() 
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
            foreach (var info in specification.WhereExpressions)
                query = query.Where(info.Filter);

            return query;
        }

        public IEnumerable<TEntity> Evaluate<TEntity>(IEnumerable<TEntity> query, ISpecification<TEntity> specification)
            where TEntity : IEntity
        {
            foreach (var info in specification.WhereExpressions)
                query = query.Where(info.FilterFunc);

            return query;
        }
    }
}