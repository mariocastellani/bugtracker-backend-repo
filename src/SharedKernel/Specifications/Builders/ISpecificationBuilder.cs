namespace SharedKernel.Specifications
{
    public interface ISpecificationBuilder<TEntity, TResult> : ISpecificationBuilder<TEntity>
        where TEntity : IEntity
    {
        new Specification<TEntity, TResult> Specification { get; }
    }

    public interface ISpecificationBuilder<TEntity>
        where TEntity : IEntity
    {
        Specification<TEntity> Specification { get; }
    }
}