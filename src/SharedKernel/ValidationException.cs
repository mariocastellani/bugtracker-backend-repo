namespace SharedKernel
{
    /// <summary>
    /// Can be throw when a business or domain rule is not met.
    /// </summary>
    public class ValidationException : Exception
    {
        public ValidationException()
            : base()
        {
        }

        public ValidationException(string message)
            : base(message)
        {
        }

        public ValidationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}