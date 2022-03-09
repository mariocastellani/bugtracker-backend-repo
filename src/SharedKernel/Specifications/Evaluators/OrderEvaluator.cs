namespace SharedKernel.Specifications
{
    public class OrderEvaluator : IEvaluator, IInMemoryEvaluator
    {
        #region Singleton

        public static OrderEvaluator Instance { get; }

        static OrderEvaluator()
        {
            Instance = new OrderEvaluator();
        }

        private OrderEvaluator() 
        { 
        }

        #endregion

        public bool IsCriteriaEvaluator
        {
            get { return false; }
        }

        public IQueryable<TEntity> GetQuery<TEntity>(IQueryable<TEntity> query, ISpecification<TEntity> specification)
            where TEntity : class, IEntity
        {
            if (specification.OrderExpressions == null)
                return query;

            if (specification.OrderExpressions.Count(x =>
                x.OrderType == OrderType.OrderBy ||
                x.OrderType == OrderType.OrderByDescending) > 1)
                throw new DuplicateOrderChainException();

            IOrderedQueryable<TEntity> orderedQuery = null;
            foreach (var info in specification.OrderExpressions)
            {
                if (info.OrderType == OrderType.OrderBy)
                    orderedQuery = query.OrderBy(info.KeySelector);

                else if (info.OrderType == OrderType.OrderByDescending)
                    orderedQuery = query.OrderByDescending(info.KeySelector);

                else if (info.OrderType == OrderType.ThenBy)
                    orderedQuery = orderedQuery.ThenBy(info.KeySelector);

                else if (info.OrderType == OrderType.ThenByDescending)
                    orderedQuery = orderedQuery.ThenByDescending(info.KeySelector);
            }

            if (orderedQuery != null)
                query = orderedQuery;

            return query;
        }

        public IEnumerable<TEntity> Evaluate<TEntity>(IEnumerable<TEntity> query, ISpecification<TEntity> specification)
            where TEntity : IEntity
        {
            if (specification.OrderExpressions == null)
                return query;

            if (specification.OrderExpressions.Count(x =>
                x.OrderType == OrderType.OrderBy ||
                x.OrderType == OrderType.OrderByDescending) > 1)
                throw new DuplicateOrderChainException();

            IOrderedEnumerable<TEntity> orderedQuery = null;
            foreach (var orderExpression in specification.OrderExpressions)
            {
                if (orderExpression.OrderType == OrderType.OrderBy)
                    orderedQuery = query.OrderBy(orderExpression.KeySelectorFunc);

                else if (orderExpression.OrderType == OrderType.OrderByDescending)
                    orderedQuery = query.OrderByDescending(orderExpression.KeySelectorFunc);

                else if (orderExpression.OrderType == OrderType.ThenBy)
                    orderedQuery = orderedQuery.ThenBy(orderExpression.KeySelectorFunc);

                else if (orderExpression.OrderType == OrderType.ThenByDescending)
                    orderedQuery = orderedQuery.ThenByDescending(orderExpression.KeySelectorFunc);
            }

            if (orderedQuery != null)
                query = orderedQuery;

            return query;
        }
    }
}