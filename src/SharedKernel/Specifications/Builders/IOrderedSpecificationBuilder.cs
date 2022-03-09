namespace SharedKernel.Specifications
{
    public interface IOrderedSpecificationBuilder<TEntity> : ISpecificationBuilder<TEntity>
        where TEntity : IEntity
    {
        bool IsChainDiscarded { get; set; }
    }
}