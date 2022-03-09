using Domain.Repositories;
using FluentValidation;

namespace Domain.Entities.Issues.Validators
{
    public class IssueValidator : AbstractValidator<Issue>
    {
        private readonly IIssueRepo _repo;

        public IssueValidator(IIssueRepo repo)
        {
            _repo = repo;

            RuleFor(x => x.Subject)
                .NotEmpty()
                .WithMessage("Subject is required.");
            
            RuleFor(x => x.Subject)
                .MustAsync(async (entity, value, cancellationToken) => 
                    await UniqueSubject(entity, cancellationToken))
                .WithMessage("Issue name duplicated.");
        }

        private async Task<bool> UniqueSubject(Issue entity, CancellationToken cancellationToken)
        {
            var issues = await _repo.GetAllAsync(cancellationToken);

            return !issues.Any(x =>
                x.Subject == entity.Subject &&
                x.Id != entity.Id);
        }
    }
}