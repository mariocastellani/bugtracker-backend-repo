namespace SharedKernel.Result
{
    public class ValidationError
    {
        public string Identifier { get; set; }

        public string ErrorMessage { get; set; }

        public ValidationSeverity Severity { get; set; }

        public ValidationError()
        {
            Severity = ValidationSeverity.Error;
        }
    }
}