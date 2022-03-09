namespace SharedKernel.Specifications
{
    public interface IInMemorySpecificationEvaluator
    {
        IEnumerable<TResult> Evaluate<TEntity, TResult>(IEnumerable<TEntity> source, ISpecification<TEntity, TResult> specification)
            where TEntity : IEntity;

        IEnumerable<TEntity> Evaluate<TEntity>(IEnumerable<TEntity> source, ISpecification<TEntity> specification)
            where TEntity : IEntity;
    }
}