using System.Net;

namespace Shared.Kernel.Result;

public record Error(string Code, string Description, ErrorType Type = ErrorType.None)
{
    public static implicit operator Result(Error error) => Result.Failure(error);
     
    public static readonly Error None = new(string.Empty, string.Empty);
    public static Error NullValue(string propertyName) => new("Error.NullValue", $"Property '{propertyName}' null value was provided", ErrorType.Failure);
 
    public static Error Failure(string description) =>
        new("Internal.Error", description, ErrorType.Failure);
     
    public static Error Validation(string code, string description) =>
        new(code, description, ErrorType.Validation);
     
    public static Error NotFound(string code, string description) =>
        new(code, description, ErrorType.NotFound);
     
    public static Error Conflict(string code, string description) =>
        new(code, description, ErrorType.Conflict);
 
    public static Error HttpResponseError(string code, string description, HttpResponseMessage response) =>
        response.StatusCode switch
        {
            HttpStatusCode.BadRequest => Validation(code, description),
            HttpStatusCode.NotFound => NotFound(code, description),
            HttpStatusCode.Conflict => Conflict(code, description),
            _ => Failure(description)
        };
}
 
public enum ErrorType
{
    None = 0,
    Failure = 1,
    Validation = 2,
    NotFound = 3,
    Conflict = 4
}