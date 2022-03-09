namespace Application.Issues
{
    public class GetAllIssuesRequest : IRequest<Result<List<IssueDto>>>
    {
    }

    public class GetAllIssuesHandler : IRequestHandler<GetAllIssuesRequest, Result<List<IssueDto>>>
    {
        private readonly IIssueRepo _repo;

        public GetAllIssuesHandler(IIssueRepo repo)
        {
            _repo = repo;
        }

        public async Task<Result<List<IssueDto>>> Handle(GetAllIssuesRequest request, CancellationToken cancellationToken)
        {
            var entities = await _repo.GetAllAsync(cancellationToken);
            
            var dtos = entities
                .Select(x => new IssueDto(x))
                .ToList();

            return Result<List<IssueDto>>.Success(dtos);
        }
    }   
}