using MassTransit;
using Microsoft.Extensions.Logging;
using Shared.Domain.Commands;
using User.Application.Contracts.Interfaces;

namespace User.Application.Consumers;

public class GetUserConsumer : BaseConsumer<IGetUser>
{
    private readonly IUserService _userService;
    private readonly ILogger<GetUserConsumer> _logger;

    public GetUserConsumer(IUserService userService, ILogger<GetUserConsumer> logger) : base(logger)
    {
        _userService = userService;
        _logger = logger;
    }
    
    protected override async Task ConsumeContext(ConsumeContext<IGetUser> context)
    {
        _logger.LogInformation("Consumed {MessageType} with data {@MessageData}", nameof(IGetUser), context.Message);
        
        var result = await _userService.Get(context.Message.Id);
        
        await context.RespondAsync(result);
        
        _logger.LogInformation("{Consumer} responded with result {@Result}", nameof(GetUserConsumer), result);
    }
}