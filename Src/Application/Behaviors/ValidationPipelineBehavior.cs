﻿using Contract.Abstractions.Shared;
using FluentValidation;
using MediatR;

namespace Application.Behaviors;
public class ValidationPipelineBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Result
{
    private readonly IEnumerable<IValidator<TRequest>> validators;

    public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators) => this.validators = validators;

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!validators.Any())
        {
            return await next();
        }

        Error[] errors = validators
            .Select(validator => validator.Validate(request))
            .SelectMany(validatorResult => validatorResult.Errors)
            .Where(validatorFailure => validatorFailure != null)
            .Select(failure => new Error(failure.PropertyName, failure.ErrorMessage))
            .Distinct()
            .ToArray();

        if (errors.Any())
        {
            var errorMessage = validators.Select(validator => validator.Validate(request))
                .SelectMany(validatorResult => validatorResult.Errors)
                .Where(validatorFailure => validatorFailure != null)
                .Select(failure => failure.ErrorMessage)
                .Distinct()
                .ToArray();
            var errorMessageString = string.Join(";", errorMessage);
            throw new ValidationException(errorMessageString);
        }

        return await next();
    }

    private static TResult CreateValidationResult<TResult>(Error[] errors)
        where TResult : Result
    {
        if (typeof(TResult) == typeof(Result))
        {
            return (ValidationResult.WithErrors(errors) as TResult)!;
        }

        object validationResult = typeof(ValidationResult<>)
            .GetGenericTypeDefinition()
            .MakeGenericType(typeof(TResult).GenericTypeArguments[0])
            .GetMethod(nameof(ValidationResult.WithErrors))!
            .Invoke(null, new object?[] { errors })!;

        return (TResult)validationResult;
    }
}
