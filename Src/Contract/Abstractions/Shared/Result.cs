namespace Contract.Abstractions.Shared;
public class Result
{
    public Result(bool isSuccess, string error, int statusCode = 200)
    {
        if (isSuccess && error != Error.None && !string.IsNullOrWhiteSpace(error))
        {
            throw new InvalidOperationException();
        }

        if (!isSuccess && error == Error.None && string.IsNullOrWhiteSpace(error))
        {
            throw new InvalidOperationException();
        }

        IsSuccess = isSuccess;
        Message = error;
        StatusCode = statusCode;
    }
    public bool IsSuccess { get; }

    public string Message { get; set; }
    public int StatusCode { get; } = 200;

    public static Result Success() => new(true, "");
    public static Result Success(int statusCode) => new(true, "", statusCode);

    public static Result<TValue> Success<TValue>(TValue value) =>
        new(value, true, "");

    public static Result<TValue> Success<TValue>(TValue value, int statusCode) =>
        new(value, true, "", statusCode);

    public static Result<TValue> Success<TValue>(int statusCode) =>
        new(default, true, "", statusCode);
    public static Result Failure(string error) =>
        new(false, error, 400);
    public static Result Failure(string error, int statusCode) =>
        new(false, error, statusCode);
    public static Result<TValue> Failure<TValue>(string error) =>
        new(default, false, error);

    public static Result<TValue> Failure<TValue>(string error, int statusCode) =>
    new(default, false, error, statusCode);

    public static Result<TValue> Create<TValue>(TValue? value) =>
        value is not null ? Success(value) : Failure<TValue>("The specified result value is null.");


}

