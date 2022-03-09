using System.Linq.Expressions;

namespace SharedKernel.Specifications
{
    /// <inheritdoc cref="ISpecification{TEntity, TResult}"/>
    public abstract class Specification<TEntity, TResult> : Specification<TEntity>, ISpecification<TEntity, TResult>
        where TEntity : IEntity
    {
        protected new virtual ISpecificationBuilder<TEntity, TResult> Query { get; }

        protected Specification()
            : this(InMemorySpecificationEvaluator.Default)
        {
        }

        protected Specification(IInMemorySpecificationEvaluator inMemorySpecificationEvaluator)
            : base(inMemorySpecificationEvaluator)
        {
            Query = new SpecificationBuilder<TEntity, TResult>(this);
        }

        public new virtual IEnumerable<TResult> Evaluate(IEnumerable<TEntity> entities)
        {
            return Evaluator.Evaluate(entities, this);
        }

        /// <inheritdoc/>
        public Expression<Func<TEntity, TResult>> Selector { get; internal set; }

        /// <inheritdoc/>
        public new Func<IEnumerable<TResult>, IEnumerable<TResult>> PostProcessingAction { get; internal set; } = null;
    }

    /// <inheritdoc cref="ISpecification{TEntity}"/>
    public abstract class Specification<TEntity> : ISpecification<TEntity> 
        where TEntity : IEntity
    {
        protected IInMemorySpecificationEvaluator Evaluator { get; }
        protected ISpecificationValidator Validator { get; }
        protected virtual ISpecificationBuilder<TEntity> Query { get; }

        protected Specification()
            : this(InMemorySpecificationEvaluator.Default, SpecificationValidator.Default)
        {
        }

        protected Specification(IInMemorySpecificationEvaluator inMemorySpecificationEvaluator)
            : this(inMemorySpecificationEvaluator, SpecificationValidator.Default)
        {
        }

        protected Specification(ISpecificationValidator specificationValidator)
            : this(InMemorySpecificationEvaluator.Default, specificationValidator)
        {
        }

        protected Specification(IInMemorySpecificationEvaluator inMemorySpecificationEvaluator, ISpecificationValidator specificationValidator)
        {
            WhereExpressions = new List<WhereExpressionInfo<TEntity>>();
            OrderExpressions = new List<OrderExpressionInfo<TEntity>>();
            IncludeExpressions = new List<IncludeExpressionInfo>();
            IncludeStrings = new List<string>();
            SearchCriterias = new List<SearchExpressionInfo<TEntity>>();

            Evaluator = inMemorySpecificationEvaluator;
            Validator = specificationValidator;
            Query = new SpecificationBuilder<TEntity>(this);
        }

        /// <inheritdoc/>
        public virtual IEnumerable<TEntity> Evaluate(IEnumerable<TEntity> entities)
        {
            return Evaluator.Evaluate(entities, this);
        }

        /// <inheritdoc/>
        public virtual bool IsSatisfiedBy(TEntity entity)
        {
            return Validator.IsValid(entity, this);
        }

        /// <inheritdoc/>
        public IEnumerable<WhereExpressionInfo<TEntity>> WhereExpressions { get; }

        /// <inheritdoc/>
        public IEnumerable<OrderExpressionInfo<TEntity>> OrderExpressions { get; }

        /// <inheritdoc/>
        public IEnumerable<IncludeExpressionInfo> IncludeExpressions { get; }

        /// <inheritdoc/>
        public IEnumerable<string> IncludeStrings { get; }

        /// <inheritdoc/>
        public IEnumerable<SearchExpressionInfo<TEntity>> SearchCriterias { get; }

        /// <inheritdoc/>
        public int? Take { get; internal set; }

        /// <inheritdoc/>
        public int? Skip { get; internal set; }

        /// <inheritdoc/>
        public Func<IEnumerable<TEntity>, IEnumerable<TEntity>> PostProcessingAction { get; internal set; }

        /// <inheritdoc/>
        public string CacheKey { get; internal set; }

        /// <inheritdoc/>
        public bool CacheEnabled { get; internal set; }

        /// <inheritdoc/>
        public bool AsNoTracking { get; internal set; }

        /// <inheritdoc/>
        public bool AsSplitQuery { get; internal set; }

        /// <inheritdoc/>
        public bool AsNoTrackingWithIdentityResolution { get; internal set; }

        /// <inheritdoc/>
        public bool IgnoreQueryFilters { get; internal set; }
    }
}