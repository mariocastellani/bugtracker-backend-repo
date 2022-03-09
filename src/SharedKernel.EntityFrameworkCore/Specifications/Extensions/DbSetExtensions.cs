namespace SharedKernel.EntityFrameworkCore.Specifications
{
    public static class DbSetExtensions
    {
        public static async Task<List<TSource>> ToListAsync<TSource>(this DbSet<TSource> source, ISpecification<TSource> specification, CancellationToken cancellationToken = default)
            where TSource : class, IEntity
        {
            var result = await SpecificationEvaluator.Default.GetQuery(source, specification).ToListAsync(cancellationToken);

            return specification.PostProcessingAction != null
                ? specification.PostProcessingAction(result).ToList()
                : result;
        }

        public static async Task<IEnumerable<TSource>> ToEnumerableAsync<TSource>(this DbSet<TSource> source, ISpecification<TSource> specification, CancellationToken cancellationToken = default)
            where TSource : class, IEntity
        {
            var result = await SpecificationEvaluator.Default.GetQuery(source, specification).ToListAsync(cancellationToken);

            return specification.PostProcessingAction != null
                ? specification.PostProcessingAction(result)
                : result;
        }

        public static IQueryable<TSource> WithSpecification<TSource>(this IQueryable<TSource> source, ISpecification<TSource> specification, ISpecificationEvaluator? evaluator = null)
            where TSource : class, IEntity
        {
            evaluator ??= SpecificationEvaluator.Default;
            return evaluator.GetQuery(source, specification);
        }
    }
}