using Domain.Entities.Issues;
using Domain.Entities.Issues.Validators;

namespace Application.Issues
{
    public class CreateIssueRequest : IRequest<Result<IssueDto>>
    {
        public int Type { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public string AssignedTo { get; set; }
    }

    public class CreateIssueHandler : IRequestHandler<CreateIssueRequest, Result<IssueDto>>
    {
        private readonly IIssueRepo _repo;
        private readonly IssueValidator _validator;

        public CreateIssueHandler(IIssueRepo repo, IssueValidator validator)
        {
            _repo = repo;
            _validator = validator;
        }

        public async Task<Result<IssueDto>> Handle(CreateIssueRequest request, CancellationToken cancellationToken)
        {
            var entity = new Issue()
            {
                Type = (IssueType)request.Type,
                Subject = request.Subject,
                Description = request.Description,
                AssignedTo = request.AssignedTo
            };

            var result = _validator.Validate(entity);
            if (!result.IsValid)
                return Result<IssueDto>.Invalid(result.ToValidationErrors());

            var savedEntity = await _repo.AddAsync(entity, cancellationToken);
            var dto = new IssueDto(savedEntity);

            return Result<IssueDto>.Success(dto);
        }
    }
}