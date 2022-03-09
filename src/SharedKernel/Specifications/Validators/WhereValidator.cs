namespace SharedKernel.Specifications
{
    public class WhereValidator : IValidator
    {
        #region Singleton

        public static WhereValidator Instance { get; }

        static WhereValidator()
        {
            Instance = new WhereValidator();
        }

        private WhereValidator() 
        { 
        }

        #endregion

        public bool IsValid<TEntity>(TEntity entity, ISpecification<TEntity> specification)
            where TEntity : IEntity
        {
            foreach (var info in specification.WhereExpressions)
                if (!info.FilterFunc(entity))
                    return false;

            return true;
        }
    }
}