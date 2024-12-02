namespace Contract.Abstractions.Shared;
public sealed class ValidationResult<TValue> : Result<TValue>, IValidationResult
{
    private ValidationResult(Error[] errors)
        : base(default, false, IValidationResult.ValidationError, 404)
    => Errors = errors;

    public Error[] Errors { get; }
    public static ValidationResult<TValue> WithErrors(Error[] errors) => new(errors);
}
