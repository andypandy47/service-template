using Shared.Domain.Result;

namespace User.Application;

public static class UserErrors
{
    public static Error NotFound(Guid id) =>
        Error.NotFound("User.NotFound", $"No user could be found with Id: {id}");
}