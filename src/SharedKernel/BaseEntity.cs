using System.ComponentModel.DataAnnotations.Schema;

namespace SharedKernel
{
    /// <summary>
    /// Base class for all entities.
    /// </summary>
    public abstract class BaseEntity : IEntity
    {
        /// <inheritdoc/>
        public int Id { get; set; }

        /// <summary>
        /// Domain events.
        /// </summary>
        [NotMapped]
        public List<BaseDomainEvent> Events { get; }

        public BaseEntity()
        {
            Events = new List<BaseDomainEvent>();
        }
    }
}