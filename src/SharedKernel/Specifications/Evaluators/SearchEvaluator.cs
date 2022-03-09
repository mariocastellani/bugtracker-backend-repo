namespace SharedKernel.Specifications
{
    public class SearchEvaluator : IInMemoryEvaluator
    {
        #region Singleton

        public static SearchEvaluator Instance { get; } = new SearchEvaluator();

        static SearchEvaluator()
        {
            Instance = new SearchEvaluator();
        }
                
        private SearchEvaluator() 
        { 
        }

        #endregion

        public IEnumerable<TEntity> Evaluate<TEntity>(IEnumerable<TEntity> query, ISpecification<TEntity> specification)
            where TEntity : IEntity
        {
            foreach (var searchGroup in specification.SearchCriterias.GroupBy(x => x.SearchGroup))
                query = query.Where(x => searchGroup.Any(s => s.SelectorFunc(x).Like(s.SearchTerm)));

            return query;
        }
    }
}