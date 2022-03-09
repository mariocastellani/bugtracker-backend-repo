namespace SharedKernel.Specifications
{
    public interface IValidator
    {
        bool IsValid<TEntity>(TEntity entity, ISpecification<TEntity> specification)
            where TEntity : IEntity;
    }
}