namespace Domain.Entities.Issues
{
    public class Attachment : BaseAuditableEntity
    {
        public int IssueId { get; set; }
        public virtual Issue Issue { get; set; }

        public string InternalFileName { get; }
        public string FileName { get; set; }

        [NotMapped]
        public byte[] FileContent { get; set; }

        public Attachment()
        {
            InternalFileName =  Guid.NewGuid().ToString("N").ToUpper();
        }
    }
}