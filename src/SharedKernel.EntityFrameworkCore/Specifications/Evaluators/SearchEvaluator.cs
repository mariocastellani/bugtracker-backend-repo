namespace SharedKernel.EntityFrameworkCore.Specifications
{
    public class SearchEvaluator : IEvaluator
    {
        #region Singleton

        public static SearchEvaluator Instance { get; }

        static SearchEvaluator()
        {
            Instance = new SearchEvaluator();
        }

        private SearchEvaluator() 
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
            foreach (var searchCriteria in specification.SearchCriterias.GroupBy(x => x.SearchGroup))
                query = query.Search(searchCriteria);

            return query;
        }
    }
}