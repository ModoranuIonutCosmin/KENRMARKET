using Customers.Application.Interfaces;
using Customers.Domain.Entities;
using IntegrationEvents.Contracts;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Customers.Application.Features;

public class ReservationsService :IReservationsService
{
    private readonly IReservationsRepository      _reservationsRepository;
    private readonly IPublishEndpoint             _publishEndpoint;
    private readonly IUnitOfWork                  _unitOfWork;
    private readonly ILogger<ReservationsService> _logger;

    public ReservationsService(IReservationsRepository reservationsRepository, IPublishEndpoint publishEndpoint, IUnitOfWork unitOfWork, ILogger<ReservationsService> logger)
    {
        _reservationsRepository = reservationsRepository;
        _publishEndpoint   = publishEndpoint;
        _unitOfWork             = unitOfWork;
        _logger                 = logger;
    }

    public async Task<int> InvalidateExpiredReservations()
    {
        int reservationsCancelled = 0;
        var reservations = await _reservationsRepository.GetAllReservations();
        
        foreach (var reservation in reservations.Where(e => e.ExpiresAt <= DateTimeOffset.UtcNow))
        {

            var reservationItems = reservation.ReservationItems
                                              .Select(r => (r.ProductId, r.Quantity));
            
            await _reservationsRepository.RemoveReservationForUser(reservation.CustomerId);
            await _publishEndpoint.Publish(new ReservationExpiredIntegrationEvent(reservationItems.ToList(), reservation.CustomerId,
                orderId: reservation.OrderId));

            reservationsCancelled++;
        }

        await _unitOfWork.CommitTransaction();

        return reservationsCancelled;
    }

    public async Task RemoveReservationByCustomerId(Guid customerId)
    {
        _logger.LogInformation("Clearing reservation for customerId={@customerId}", customerId);
        
        await _reservationsRepository.RemoveReservationForUser(customerId);

        await _unitOfWork.CommitTransaction();
        
        _logger.LogInformation("Cleared reservation for customerId={@customerId}", customerId);

    }
    
    public async Task MakeReservationsForCustomer(Guid customerId, DateTimeOffset createdAt, DateTimeOffset expiresAt, bool isCanceled,
        List<ReservationItem> reservationItems, Guid orderId)
    {
        _logger.LogInformation("Creating a reservation for customerId={@customerId}", customerId);
            var reservation = new Reservation(
                                              customerId: customerId,
                                              reservedAt:   createdAt,
                                              expiresAt:    expiresAt,
                                              canceled: isCanceled,
                                              reservationItems: reservationItems,
                                              orderId: orderId
                                             );

            await _reservationsRepository.UpdateReservationsForCustomer(customerId, reservation);

            await _unitOfWork.CommitTransaction();
            
            _logger.LogInformation("Finished created a reservation for customerId={@customerId}", customerId);

    }

    public async Task<List<Reservation>> GetAllReservations()
    {
        return (await _reservationsRepository.GetAllAsync()).ToList();
    }
}