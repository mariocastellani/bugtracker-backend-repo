namespace SharedKernel.Specifications
{
    public interface ICacheSpecificationBuilder<TEntity> : ISpecificationBuilder<TEntity> 
        where TEntity : IEntity
    {
        bool IsChainDiscarded { get; set; }
    }
}