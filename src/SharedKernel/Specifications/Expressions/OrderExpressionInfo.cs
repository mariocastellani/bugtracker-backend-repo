using System.Linq.Expressions;

namespace SharedKernel.Specifications
{
    /// <summary>
    /// Encapsulates data needed to perform sorting.
    /// </summary>
    /// <typeparam name="TEntity">Type of the entity to apply sort on.</typeparam>
    public class OrderExpressionInfo<TEntity>
    {
        private readonly Lazy<Func<TEntity, object>> _keySelectorFunc;

        /// <summary>
        /// A function to extract a key from an element.
        /// </summary>
        public Expression<Func<TEntity, object>> KeySelector { get; }

        /// <summary>
        /// Whether to (subsequently) sort ascending or descending.
        /// </summary>
        public OrderType OrderType { get; }

        /// <summary>
        /// Compiled <see cref="KeySelector" />.
        /// </summary>
        public Func<TEntity, object> KeySelectorFunc
        {
            get { return _keySelectorFunc.Value; }
        }

        /// <summary>
        /// Creates instance of <see cref="OrderExpressionInfo{TEntity}" />.
        /// </summary>
        /// <param name="keySelector">A function to extract a key from an element.</param>
        /// <param name="orderType">Whether to (subsequently) sort ascending or descending.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="keySelector"/> is null.</exception>
        public OrderExpressionInfo(Expression<Func<TEntity, object>> keySelector, OrderType orderType)
        {
            KeySelector = keySelector ?? throw new ArgumentNullException(nameof(keySelector));
            OrderType = orderType;

            _keySelectorFunc = new Lazy<Func<TEntity, object>>(KeySelector.Compile);
        }
    }
}