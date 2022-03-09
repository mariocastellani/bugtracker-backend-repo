using System.Linq;

namespace SharedKernel.Specifications
{
    public interface IEvaluator
    {
        bool IsCriteriaEvaluator { get; }

        IQueryable<TEntity> GetQuery<TEntity>(IQueryable<TEntity> query, ISpecification<TEntity> specification) 
            where TEntity : class, IEntity;
    }
}