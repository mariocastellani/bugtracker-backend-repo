namespace SharedKernel.Specifications
{
    /// <summary>
    /// Evaluates the logic encapsulated by an <see cref="ISpecification{TEntity}"/>.
    /// </summary>
    public interface ISpecificationEvaluator
    {
        /// <summary>
        /// Applies the logic encapsulated by <paramref name="specification"/> to given <paramref name="inputQuery"/>,
        /// and projects the result into <typeparamref name="TResult"/>.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="inputQuery">The sequence of <typeparamref name="TEntity"/>.</param>
        /// <param name="specification">The encapsulated query logic.</param>
        /// <returns>A filtered sequence of <typeparamref name="TResult"/>.</returns>
        IQueryable<TResult> GetQuery<TEntity, TResult>(IQueryable<TEntity> inputQuery, ISpecification<TEntity, TResult> specification) 
            where TEntity : class, IEntity;

        /// <summary>
        /// Applies the logic encapsulated by <paramref name="specification"/> to given <paramref name="inputQuery"/>.
        /// </summary>
        /// <param name="inputQuery">The sequence of <typeparamref name="TEntity"/>.</param>
        /// <param name="specification">The encapsulated query logic.</param>
        /// <returns>A filtered sequence of <typeparamref name="TEntity"/>.</returns>
        IQueryable<TEntity> GetQuery<TEntity>(IQueryable<TEntity> inputQuery, ISpecification<TEntity> specification, bool evaluateCriteriaOnly = false) 
            where TEntity : class, IEntity;
    }
}