using Customers.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Customers.Application.Workers;

public class CancelExpiringReservationsWorker : BackgroundService
{
    private readonly IServiceProvider                          _serviceProvider;
    private readonly ILogger<CancelExpiringReservationsWorker> _logger;

    public CancelExpiringReservationsWorker(IServiceProvider serviceProvider, ILogger<CancelExpiringReservationsWorker> logger)
    {
        _serviceProvider = serviceProvider;
        _logger               = logger;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using IServiceScope scope = _serviceProvider.CreateScope();

        var reservationsService = scope.ServiceProvider.GetRequiredService<IReservationsService>();
        
        while (!stoppingToken.IsCancellationRequested)
        {
            var cancelledCount = await reservationsService.InvalidateExpiredReservations();
            
            _logger.LogInformation("[Clear expiring reservations] Cleared {@count} expired reservations.", cancelledCount);

            await Task.Delay(30000, stoppingToken);
        }
    }
}