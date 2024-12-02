namespace Contract.Abstractions.Shared;
public sealed class ValidationResult : Result, IValidationResult
{
    private ValidationResult(Error[] errors)
        : base(false, IValidationResult.ValidationError, 404)
    => Errors = errors;

    public Error[] Errors { get; }
    public static ValidationResult WithErrors(Error[] errors) => new(errors);
}
