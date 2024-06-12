using Microsoft.AspNetCore.Mvc;
using User.Api.Extensions;
using User.Api.Models.Requests;
using User.Application.Contracts.Interfaces;

namespace User.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController(IUserService userService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await userService.Get(Guid.NewGuid());

        if (result.IsFailure)
        {
            return result.ToProblemDetails();
        }
        
        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserRequest requestBody)
    {
        return Created();
    }
}

