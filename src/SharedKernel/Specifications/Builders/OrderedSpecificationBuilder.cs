namespace SharedKernel.Specifications
{
    public class OrderedSpecificationBuilder<TEntity> : IOrderedSpecificationBuilder<TEntity>
        where TEntity : IEntity
    {
        public Specification<TEntity> Specification { get; }

        public bool IsChainDiscarded { get; set; }

        public OrderedSpecificationBuilder(Specification<TEntity> specification)
            : this(specification, false)
        {
        }

        public OrderedSpecificationBuilder(Specification<TEntity> specification, bool isChainDiscarded)
        {
            Specification = specification;
            IsChainDiscarded = isChainDiscarded;
        }
    }
}