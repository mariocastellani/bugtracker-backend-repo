namespace SharedKernel.Specifications
{
    public class SearchValidator : IValidator
    {
        #region Singleton

        public static SearchValidator Instance { get; }

        static SearchValidator()
        {
            Instance = new SearchValidator();
        }

        private SearchValidator() 
        { 
        }

        #endregion

        public bool IsValid<TEntity>(TEntity entity, ISpecification<TEntity> specification)
            where TEntity : IEntity
        {
            foreach (var searchGroup in specification.SearchCriterias.GroupBy(x => x.SearchGroup))
                if (searchGroup.Any(x => !x.SelectorFunc(entity).Like(x.SearchTerm)))
                    return false;

            return true;
        }
    }
}