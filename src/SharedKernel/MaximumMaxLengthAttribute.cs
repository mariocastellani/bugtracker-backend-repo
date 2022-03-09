namespace SharedKernel
{
    /// <summary>
    /// Useful to indicate that a string property of an entity must be mapping to a nvarchar(max) database type.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class MaximumMaxLengthAttribute : Attribute
    {
    }
}
