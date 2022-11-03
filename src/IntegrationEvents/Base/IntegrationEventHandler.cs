using System.Threading.Tasks;
using MassTransit;

namespace IntegrationEvents.Base;

public abstract class IntegrationEventHandler<T> : IConsumer<T>
    where T : class

{
    public async Task Consume(ConsumeContext<T> context)
    {
        await Handle(context.Message);
    }

    public abstract Task Handle(T @event);
}