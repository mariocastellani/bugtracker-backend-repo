namespace SharedKernel.EntityFrameworkCore.Specifications
{
    /// <inheritdoc cref="ISpecificationEvaluator"/>
    public class SpecificationEvaluator : ISpecificationEvaluator
    {
        #region Default Evaluators

        // Will use singleton for default configuration. Yet, it can be instantiated if necessary, with default or provided evaluators.
        /// <summary>
        /// <see cref="SpecificationEvaluator" /> instance with default evaluators and without any additional features enabled.
        /// </summary>
        public static SpecificationEvaluator Default { get; }

        /// <summary>
        /// <see cref="SpecificationEvaluator" /> instance with default evaluators and enabled caching.
        /// </summary>
        public static SpecificationEvaluator Cached { get; }

        static SpecificationEvaluator()
        {
            Default = new SpecificationEvaluator();
            Cached = new SpecificationEvaluator(true);
        }

        #endregion

        private readonly List<IEvaluator> _evaluators;

        public SpecificationEvaluator(bool cacheEnabled = false)
        {
            _evaluators = new List<IEvaluator>(new IEvaluator[]
            {
                WhereEvaluator.Instance,
                SearchEvaluator.Instance,
                cacheEnabled ? IncludeEvaluator.Cached : IncludeEvaluator.Default,
                OrderEvaluator.Instance,
                PaginationEvaluator.Instance,
                AsNoTrackingEvaluator.Instance,
                IgnoreQueryFiltersEvaluator.Instance,
                AsSplitQueryEvaluator.Instance,
                AsNoTrackingWithIdentityResolutionEvaluator.Instance
            });
        }

        public SpecificationEvaluator(IEnumerable<IEvaluator> evaluators)
        {
            _evaluators = new List<IEvaluator>(evaluators);
        }

        /// <inheritdoc/>
        public virtual IQueryable<TResult> GetQuery<TEntity, TResult>(IQueryable<TEntity> query, ISpecification<TEntity, TResult> specification) 
            where TEntity : class, IEntity
        {
            if (specification == null)
                throw new ArgumentNullException(nameof(specification));

            if (specification.Selector == null)
                throw new SelectorNotFoundException();

            query = GetQuery(query, (ISpecification<TEntity>)specification);

            return query.Select(specification.Selector);
        }

        /// <inheritdoc/>
        public virtual IQueryable<TEntity> GetQuery<TEntity>(IQueryable<TEntity> query, ISpecification<TEntity> specification, bool evaluateCriteriaOnly = false) 
            where TEntity : class, IEntity
        {
            if (specification == null)
                throw new ArgumentNullException(nameof(specification));

            var evaluators = evaluateCriteriaOnly 
                ? _evaluators.Where(x => x.IsCriteriaEvaluator) 
                : _evaluators;

            foreach (var evaluator in evaluators)
                query = evaluator.GetQuery(query, specification);

            return query;
        }
    }
}