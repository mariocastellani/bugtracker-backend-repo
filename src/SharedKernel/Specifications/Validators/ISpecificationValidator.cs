namespace SharedKernel.Specifications
{
    public interface ISpecificationValidator
    {
        bool IsValid<TEntity>(TEntity entity, ISpecification<TEntity> specification)
            where TEntity : IEntity;
    }
}