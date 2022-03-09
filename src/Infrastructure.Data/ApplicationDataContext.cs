using Domain.Entities.Issues;

namespace Infrastructure.Data
{
    public class ApplicationDataContext : BaseDbContext
    {
        public ApplicationDataContext(DbContextOptions<ApplicationDataContext> options, IMediator mediator)
            : base(options, mediator)
        {
        }

        protected DbSet<Issue> Issues => Set<Issue>();
        protected DbSet<Attachment> Attachments => Set<Attachment>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


        }

        public override void Dispose()
        {
            base.Dispose();
        }

        public override ValueTask DisposeAsync()
        {
            return base.DisposeAsync();
        }
    }
}