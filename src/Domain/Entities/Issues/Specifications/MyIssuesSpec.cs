using SharedKernel.Specifications;

namespace Domain.Entities.Issues.Specifications
{
    public class MyIssuesSpec : Specification<Issue>
    {
        public MyIssuesSpec(string myUserName)
        {
            Query
                .Where(x => x.AssignedTo == myUserName)
                .OrderBy(x => x.Created);
        }
    }
}