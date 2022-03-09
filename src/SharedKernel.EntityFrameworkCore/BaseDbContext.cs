using MediatR;

namespace SharedKernel.EntityFrameworkCore
{
    public abstract class BaseDbContext : DbContext
    {
        private readonly IMediator _mediator;

        public BaseDbContext(DbContextOptions options, IMediator mediator)
            : base(options)
        {
            _mediator = mediator;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Change default cascade delete behaviour to restrict
            modelBuilder.SetDefaultDeleteBehaviorInForeignKeys(DeleteBehavior.Restrict);

            // Sets default max-length to 100 for all string properties in the model
            modelBuilder.SetDefaultMaxLengthOfStringProperties(100);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            // Ignore events if no dispatcher provided
            if (_mediator == null)
                return result;

            // Find entities with domain events
            var entitiesWithEvents = ChangeTracker.Entries<BaseEntity>()
                .Select(x => x.Entity)
                .Where(x => x.Events.Any())
                .ToArray();

            // Dispatch domain events only if save was successful
            foreach (var entity in entitiesWithEvents)
            {
                var events = entity.Events.ToArray();
                entity.Events.Clear();
                foreach (var domainEvent in events)
                    await _mediator.Publish(domainEvent).ConfigureAwait(false);
            }

            return result;
        }

        public override int SaveChanges()
        {
            return SaveChangesAsync().GetAwaiter().GetResult();
        }
    }
}