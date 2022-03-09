namespace SharedKernel.Specifications
{
    public class InMemorySpecificationEvaluator : IInMemorySpecificationEvaluator
    {
        #region Default

        /// <summary>
        /// Will use singleton for default configuration.
        /// However, it can be instantiated if necessary, 
        /// with default or provided evaluators.
        /// </summary>
        public static InMemorySpecificationEvaluator Default { get; }

        static InMemorySpecificationEvaluator()
        {
            Default = new InMemorySpecificationEvaluator();
        }

        #endregion

        private readonly List<IInMemoryEvaluator> _evaluators;

        public InMemorySpecificationEvaluator()
        {
            _evaluators = new List<IInMemoryEvaluator>(new IInMemoryEvaluator[]
            {
                WhereEvaluator.Instance,
                SearchEvaluator.Instance,
                OrderEvaluator.Instance,
                PaginationEvaluator.Instance
            });
        }

        public InMemorySpecificationEvaluator(IEnumerable<IInMemoryEvaluator> evaluators)
        {
            _evaluators = new List<IInMemoryEvaluator>(evaluators);
        }

        public virtual IEnumerable<TResult> Evaluate<TEntity, TResult>(IEnumerable<TEntity> source, ISpecification<TEntity, TResult> specification)
            where TEntity : IEntity
        {
            _ = specification.Selector ?? throw new SelectorNotFoundException();

            var baseQuery = Evaluate(source, (ISpecification<TEntity>)specification);

            var resultQuery = baseQuery.Select(specification.Selector.Compile());

            return specification.PostProcessingAction != null
                ? specification.PostProcessingAction(resultQuery)
                : resultQuery;
        }

        public virtual IEnumerable<TEntity> Evaluate<TEntity>(IEnumerable<TEntity> source, ISpecification<TEntity> specification)
            where TEntity : IEntity
        {
            foreach (var evaluator in _evaluators)
                source = evaluator.Evaluate(source, specification);

            return specification.PostProcessingAction != null
                ? specification.PostProcessingAction(source)
                : source;
        }
    }
}