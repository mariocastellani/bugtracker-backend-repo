using System.Linq.Expressions;

namespace SharedKernel.Specifications
{
    /// <summary>
    /// Encapsulates data needed to perform filtering.
    /// </summary>
    /// <typeparam name="TEntity">Type of the entity to apply filter on.</typeparam>
    public class WhereExpressionInfo<TEntity>
    {
        private readonly Lazy<Func<TEntity, bool>> _filterFunc;

        /// <summary>
        /// Condition which should be satisfied by instances of <typeparamref name="TEntity"/>.
        /// </summary>
        public Expression<Func<TEntity, bool>> Filter { get; }

        /// <summary>
        /// Compiled <see cref="Filter" />.
        /// </summary>
        public Func<TEntity, bool> FilterFunc
        {
            get { return _filterFunc.Value; }
        }

        /// <summary>
        /// Creates instance of <see cref="WhereExpressionInfo{TEntity}" />.
        /// </summary>
        /// <param name="filter">Condition which should be satisfied by instances of <typeparamref name="TEntity"/>.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="filter"/> is null.</exception>
        public WhereExpressionInfo(Expression<Func<TEntity, bool>> filter)
        {
            Filter = filter ?? throw new ArgumentNullException(nameof(filter));
            _filterFunc = new Lazy<Func<TEntity, bool>>(Filter.Compile);
        }
    }
}