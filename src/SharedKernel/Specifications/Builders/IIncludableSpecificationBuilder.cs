namespace SharedKernel.Specifications
{
    public interface IIncludableSpecificationBuilder<TEntity, out TProperty> : ISpecificationBuilder<TEntity>
        where TEntity : IEntity
    {
        bool IsChainDiscarded { get; set; }
    }
}