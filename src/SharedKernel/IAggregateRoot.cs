namespace SharedKernel
{
    /// <summary>
    /// Marker interface for aggregate root entities
    /// Repositories will only work with aggregate roots, not their children.
    /// </summary>
    public interface IAggregateRoot
    {
    }
}