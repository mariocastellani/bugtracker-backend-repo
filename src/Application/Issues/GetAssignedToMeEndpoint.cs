using Domain.Entities.Issues.Specifications;

namespace Application.Issues
{
    public class GetAssignedToMeRequest : IRequest<Result<List<IssueDto>>>
    {
        public string MyUserName { get; set; }
    }

    public class GetAssignedToMeHandler : IRequestHandler<GetAssignedToMeRequest, Result<List<IssueDto>>>
    {
        private readonly IIssueRepo _repo;

        public GetAssignedToMeHandler(IIssueRepo repo)
        {
            _repo = repo;
        }

        public async Task<Result<List<IssueDto>>> Handle(GetAssignedToMeRequest request, CancellationToken cancellationToken)
        {
            var spec = new MyIssuesSpec(request.MyUserName);

            var entities = await _repo.GetBySpecAsync(spec, cancellationToken);

            var dtos = entities
                .Select(x => new IssueDto(x))
                .ToList();

            return Result<List<IssueDto>>.Success(dtos);
        }
    }
}
