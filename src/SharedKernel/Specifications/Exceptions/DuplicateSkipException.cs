namespace SharedKernel.Specifications
{
    public class DuplicateSkipException : Exception
    {
        private const string message = "Duplicate use of Skip(). Ensure you don't use Skip() more than once in the same specification.";

        public DuplicateSkipException()
            : base(message)
        {
        }

        public DuplicateSkipException(Exception innerException)
            : base(message, innerException)
        {
        }
    }
}