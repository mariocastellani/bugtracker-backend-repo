namespace SharedKernel.Specifications
{
    public class SpecificationBuilder<TEntity, TResult> : SpecificationBuilder<TEntity>, ISpecificationBuilder<TEntity, TResult>
        where TEntity : IEntity
    {
        public new Specification<TEntity, TResult> Specification { get; }

        public SpecificationBuilder(Specification<TEntity, TResult> specification)
            : base(specification)
        {
            Specification = specification;
        }
    }

    public class SpecificationBuilder<TEntity> : ISpecificationBuilder<TEntity>
        where TEntity : IEntity
    {
        public Specification<TEntity> Specification { get; }

        public SpecificationBuilder(Specification<TEntity> specification)
        {
            Specification = specification;
        }
    }
}