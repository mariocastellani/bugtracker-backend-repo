namespace SharedKernel.Specifications
{
    public class CacheSpecificationBuilder<TEntity> : ICacheSpecificationBuilder<TEntity> 
        where TEntity : IEntity
    {
        public Specification<TEntity> Specification { get; }

        public bool IsChainDiscarded { get; set; }

        public CacheSpecificationBuilder(Specification<TEntity> specification)
            : this(specification, false)
        {
        }

        public CacheSpecificationBuilder(Specification<TEntity> specification, bool isChainDiscarded)
        {
            Specification = specification;
            IsChainDiscarded = isChainDiscarded;
        }
    }
}