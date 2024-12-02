namespace Application.Exceptions;
public abstract class ValidationException : Exception
{
    public ValidationException(IReadOnlyCollection<ValidationError> errors)
        : base("One or more validation errors occurred")
        => Errors = errors;

    public IReadOnlyCollection<ValidationError> Errors { get; }
}

public record ValidationError(string PropertyName, string ErrorMessage);
