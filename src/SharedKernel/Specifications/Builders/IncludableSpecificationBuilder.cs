namespace SharedKernel.Specifications
{
    public class IncludableSpecificationBuilder<TEntity, TProperty> : IIncludableSpecificationBuilder<TEntity, TProperty>
        where TEntity : IEntity
    {
        public Specification<TEntity> Specification { get; }

        public bool IsChainDiscarded { get; set; }

        public IncludableSpecificationBuilder(Specification<TEntity> specification)
            : this(specification, false)
        {
        }

        public IncludableSpecificationBuilder(Specification<TEntity> specification, bool isChainDiscarded)
        {
            Specification = specification;
            IsChainDiscarded = isChainDiscarded;
        }
    }
}