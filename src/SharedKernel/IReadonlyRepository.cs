using SharedKernel.Specifications;

namespace SharedKernel
{
    /// <summary>
    /// Define a readonly repository that can be used to query entities.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity being operated on by this repository.</typeparam>
    public interface IReadonlyRepository<TEntity> 
        where TEntity : IEntity, IAggregateRoot
    {
        /// <summary>
        /// Finds an entity with the given <paramref name="id"/> value.
        /// </summary>
        /// <param name="id">The primary key for the entity to be found.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Finds an entity that matches the encapsulated query logic of the <paramref name="specification"/>.
        /// </summary>
        /// <param name="specification">The encapsulated query logic.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task<TEntity> GetFirstBySpecAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);

        /// <summary>
        /// Finds an entity that matches the encapsulated query logic of the <paramref name="specification"/>
        /// and projects it into a new form, being <typeparamref name="TResult" />.
        /// </summary>
        /// <typeparam name="TResult">The type of the value returned by the projection.</typeparam>
        /// <param name="specification">The encapsulated query logic.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task<TResult> GetFirstBySpecAsync<TResult>(ISpecification<TEntity, TResult> specification, CancellationToken cancellationToken = default);

        /// <summary>
        /// Finds all entities from the database.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Finds all entities that matches the encapsulated query logic of the <paramref name="specification"/>.
        /// </summary>
        /// <param name="specification">The encapsulated query logic.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task<List<TEntity>> GetBySpecAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);

        /// <summary>
        /// Finds all entities that matches the encapsulated query logic of the <paramref name="specification"/>
        /// and projects each one into a new form, being <typeparamref name="TResult" />.
        /// </summary>
        /// <typeparam name="TResult">The type of the value returned by the projection.</typeparam>
        /// <param name="specification">The encapsulated query logic.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task<List<TResult>> GetBySpecAsync<TResult>(ISpecification<TEntity, TResult> specification, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns the total number of records.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task<int> CountAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns how many entities satisfy the encapsulated query logic of the <paramref name="specification"/>.
        /// </summary>
        /// <param name="specification">The encapsulated query logic.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task<int> CountAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns a boolean whether any entity exists or not.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task<bool> AnyAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns a boolean that represents whether any entity satisfy 
        /// the encapsulated query logic of the <paramref name="specification"/> or not.
        /// </summary>
        /// <param name="specification">The encapsulated query logic.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task<bool> AnyAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);
    }
}