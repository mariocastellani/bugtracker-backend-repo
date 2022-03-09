namespace SharedKernel
{
    /// <summary>
    /// Useful to mark a <see cref="ValueObject"/> property which is ignored when comparing two objects.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class IgnoreMemberAttribute : Attribute
    {
    }
}