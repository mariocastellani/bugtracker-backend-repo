using MediatR;

namespace SharedKernel
{
    /// <summary>
    /// Base class for all domain events.
    /// </summary>
    public abstract class BaseDomainEvent : INotification
    {
        /// <summary>
        /// Timestamp of occurrence.
        /// </summary>
        public DateTime TimeStamp { get; protected set; }

        public BaseDomainEvent()
        {
            TimeStamp = DateTime.UtcNow;
        }
    }
}