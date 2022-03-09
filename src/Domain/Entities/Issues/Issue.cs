namespace Domain.Entities.Issues
{
    public class Issue : BaseAuditableEntity, IAggregateRoot
    {
        public IssueType Type { get; set; }

        public string Subject { get; set; }

        [MaximumMaxLength]
        public string Description { get; set; }

        public string AssignedTo { get; set; }

        public virtual ICollection<Attachment> Attachments { get; set; }
    }
}