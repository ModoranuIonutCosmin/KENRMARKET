using MassTransit.Mediator;
using Microsoft.Extensions.Hosting;
using Order.Application.Commands;
using Order.Application.Interfaces;

namespace Order.Application.Workers
{
    public class ClearHangingOrdersWorker: BackgroundService
    {
        private readonly IMediator _mediator;

        private static TimeSpan maxOrderAge = TimeSpan.FromHours(1);

        public ClearHangingOrdersWorker(IMediator mediator)
        {
            _mediator = mediator;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            ClearHangingOrdersCommand clearHangingOrdersCommand = new ClearHangingOrdersCommand()
            {
                MaxOrderAge = maxOrderAge
            };

            while (!stoppingToken.IsCancellationRequested)
            {
                //clear hanging orders command handling

                await _mediator.Send(clearHangingOrdersCommand);

                await Task.Delay(maxOrderAge);
            }

        }
    }
}

