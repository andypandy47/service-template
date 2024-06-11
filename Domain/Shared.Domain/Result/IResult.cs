namespace Shared.Domain.Result;

public interface IResult
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; }
}

public interface IResult<out T> : IResult
{
    public T Value { get; }
}