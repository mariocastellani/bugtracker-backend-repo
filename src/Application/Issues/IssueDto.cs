using Domain.Entities.Issues;

namespace Application.Issues
{
    public class IssueDto : EntityDto
    {
        public EnumDto Type { get; set; }

        public string Subject { get; set; }

        public string Description { get; set; }

        public string AssignedTo { get; set; }


        public IssueDto()
        {
            Type = new EnumDto();
        }

        public IssueDto(Issue entity)
            : base(entity)
        {
            Type = entity.Type.ToEnumDto();
            Subject = entity.Subject;
            Description = entity.Description;
            AssignedTo = entity.AssignedTo;
        }
    }
}