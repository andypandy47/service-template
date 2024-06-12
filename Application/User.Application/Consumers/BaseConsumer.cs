using MassTransit;
using Microsoft.Extensions.Logging;
using Shared.Kernel.Result;

namespace User.Application.Consumers;

public abstract class BaseConsumer<T> : IConsumer<T> where T : class
{
    private readonly ILogger<BaseConsumer<T>> _logger;

    protected BaseConsumer(ILogger<BaseConsumer<T>> logger)
    {
        _logger = logger;
    }
    
    protected abstract Task ConsumeContext(ConsumeContext<T> context);
    
    public async Task Consume(ConsumeContext<T> context)
    {
        try
        {
            await ConsumeContext(context);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Something went wrong! {ExceptionMessage}", e.Message);
            await context.RespondAsync<IResult>(Result.From(Error.Failure(e.Message)));
        }
    }
}