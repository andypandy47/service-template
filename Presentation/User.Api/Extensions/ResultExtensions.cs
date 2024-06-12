using System.Net;
using Microsoft.AspNetCore.Mvc;
using Shared.Kernel.Result;
using IResult = Shared.Kernel.Result.IResult;

namespace User.Api.Extensions;

public static class ResultExtensions
{
    public static IActionResult ToProblemDetails(this IResult result)
    {
        if (result.IsSuccess)
        {
            throw new InvalidOperationException();
        }

        var problemDetails = new ProblemDetails
        {
            Status = GetStatusCode(result.Error.Type),
            Title = GetTitle(result.Error.Type),
            Type = GetType(result.Error.Type),
            Extensions =
            {
                { "errors", new [] 
                    { 
                        new
                        {
                            Code = result.Error.Code,
                            Description = result.Error.Description
                        } 
                    } 
                }
            }
        };

        return problemDetails.Status switch
        {
            (int)HttpStatusCode.BadRequest => new BadRequestObjectResult(problemDetails),
            (int)HttpStatusCode.NotFound => new NotFoundObjectResult(problemDetails),
            (int)HttpStatusCode.Conflict => new ConflictObjectResult(problemDetails),
            _ => new ObjectResult(problemDetails) { StatusCode = problemDetails.Status }
        };
    }

    private static int GetStatusCode(ErrorType errorType) =>
        errorType switch
        {
            ErrorType.Validation => (int)HttpStatusCode.BadRequest,
            ErrorType.NotFound => (int)HttpStatusCode.NotFound,
            ErrorType.Conflict => (int)HttpStatusCode.Conflict,
            _ => (int)HttpStatusCode.InternalServerError,
        };
    
    private static string GetTitle(ErrorType errorType) =>
        errorType switch
        {
            ErrorType.Validation => "Bad Request",
            ErrorType.NotFound => "Not Found",
            ErrorType.Conflict => "Conflict",
            _ => "Server Failure",
        };
    
    private static string GetType(ErrorType errorType) =>
        errorType switch
        {
            ErrorType.Validation => "https://www.rfc-editor.org/rfc/rfc7231.html#section-6.5.1",
            ErrorType.NotFound => "https://www.rfc-editor.org/rfc/rfc7231.html#section-6.5.4",
            ErrorType.Conflict => "https://www.rfc-editor.org/rfc/rfc7231.html#section-6.5.8",
            _ => "https://www.rfc-editor.org/rfc/rfc7231.html#section-6.6.1",
        };
}