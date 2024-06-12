namespace Shared.Kernel.Result;

public record Result : IResult
{
    protected Result(bool isSuccess, Error error)
    {
        if (isSuccess ^ error == Error.None)
        {
            throw new ArgumentException("Invalid error", nameof(error));
        }
    
        IsSuccess = isSuccess;
        Error = error;
    }
    
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; }
    public static Result Success => new(true, Error.None);
    public static Result Failure(Error error) => new(false, error);
    public static Result<T> Failure<T>(Error error) => new(default, error);
    public static Result<T> From<T>(T value) => new(value, Error.None);
}

public record Result<T> : Result, IResult<T>
{
    public Result(T value, Error error) : base(error == Error.None, error)
    {
        Value = value;
    }
    
    public static implicit operator Result<T>(Error error) => Result.Failure<T>(error);
    public T Value { get; }
}