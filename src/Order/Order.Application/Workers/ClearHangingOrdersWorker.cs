using MassTransit.Mediator;
using Microsoft.Extensions.Hosting;
using Order.Application.Commands;

namespace Order.Application.Workers;

public class ClearHangingOrdersWorker : BackgroundService
{
    private static readonly TimeSpan MaxOrderAge = TimeSpan.FromHours(1);
    private readonly IMediator _mediator;

    public ClearHangingOrdersWorker(IMediator mediator)
    {
        _mediator = mediator;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var clearHangingOrdersCommand = new ClearHangingOrdersCommand
        {
            MaxOrderAge = MaxOrderAge
        };

        while (!stoppingToken.IsCancellationRequested)
        {
            //clear hanging orders command handling

            await _mediator.Send(clearHangingOrdersCommand, stoppingToken);

            await Task.Delay(MaxOrderAge, stoppingToken);
        }
    }
}