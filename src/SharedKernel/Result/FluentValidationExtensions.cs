using FluentValidation;
using FluentValidation.Results;

namespace SharedKernel.Result
{
    public static class FluentValidationExtensions
    {
        public static List<ValidationError> ToValidationErrors(this ValidationResult validationResult)
        {
            return validationResult.Errors
                .Select(x => new ValidationError()
                {
                    Severity = x.Severity.ToValidationSeverity(),
                    ErrorMessage = x.ErrorMessage,
                    Identifier = x.PropertyName
                })
                .ToList();
        }

        public static ValidationSeverity ToValidationSeverity(this Severity severity)
        {
            switch (severity)
            {
                case Severity.Error:
                    return ValidationSeverity.Error;

                case Severity.Warning:
                    return ValidationSeverity.Warning;

                case Severity.Info:
                    return ValidationSeverity.Info;

                default:
                    throw new ArgumentOutOfRangeException(nameof(severity), "Unexpected Severity.");
            }
        }
    }
}