namespace SharedKernel.Specifications
{
    public interface IInMemoryEvaluator
    {
        IEnumerable<TEntity> Evaluate<TEntity>(IEnumerable<TEntity> query, ISpecification<TEntity> specification)
            where TEntity : IEntity;
    }
}