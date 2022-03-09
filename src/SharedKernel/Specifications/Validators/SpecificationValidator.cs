namespace SharedKernel.Specifications
{
    public class SpecificationValidator : ISpecificationValidator
    {
        #region Default

        /// <summary>
        /// Will use singleton for default configuration. 
        /// However, it can be instantiated if necessary, 
        /// with default or provided validators. 
        /// </summary>
        public static SpecificationValidator Default { get; }

        static SpecificationValidator()
        {
            Default = new SpecificationValidator();
        }

        #endregion

        private readonly List<IValidator> _validators;

        public SpecificationValidator()
        {
            _validators = new List<IValidator>(new IValidator[]
            {
                WhereValidator.Instance,
                SearchValidator.Instance
            });
        }

        public SpecificationValidator(IEnumerable<IValidator> validators)
        {
            _validators = new List<IValidator>(validators);
        }

        public virtual bool IsValid<TEntity>(TEntity entity, ISpecification<TEntity> specification)
            where TEntity : IEntity
        {
            foreach (var partialValidator in _validators)
                if (!partialValidator.IsValid(entity, specification))
                    return false;

            return true;
        }
    }
}