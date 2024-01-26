namespace Application.Exceptions;

public class ValidationException : Exception
{
    public ValidationException(IReadOnlyCollection<ValidationErrors> errors)
        : base("Validation Failure")
    {
        Errors = errors;
    }
    public IReadOnlyCollection<ValidationErrors> Errors { get; }
}
public record ValidationErrors(string propertyName, string ErrorMessage);
