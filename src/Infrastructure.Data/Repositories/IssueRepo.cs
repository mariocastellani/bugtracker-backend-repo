using Domain.Entities.Issues;

namespace Infrastructure.Data.Repositories
{
    public class IssueRepo : BaseRepository<Issue>, IIssueRepo
    {
        public IssueRepo(ApplicationDataContext dbContext)
            : base(dbContext)
        {
        }
    }
}