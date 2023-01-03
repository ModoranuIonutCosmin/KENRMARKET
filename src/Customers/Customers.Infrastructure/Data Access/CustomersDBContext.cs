using Customers.Domain.Entities;
using Customers.Domain.Shared;
using Customers.Infrastructure.Data_Access.Config;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Customers.Infrastructure.Data_Access;

public class CustomersDBContext : DbContext
{
    private readonly IMediator _mediator;

    public CustomersDBContext()
    {
    }

    public CustomersDBContext(DbContextOptions<CustomersDBContext> options, IMediator mediator) : base(options)
    {
        _mediator = mediator;
    }

    public DbSet<Address>     Addresses    { get; set; }
    public DbSet<Customer>    Customers    { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<ReservationItem> ReservationItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.AddInboxStateEntity();
        modelBuilder.AddOutboxMessageEntity();
        modelBuilder.AddOutboxStateEntity();

        modelBuilder.ApplyConfiguration(new AddressEntityConfig());
        modelBuilder.ApplyConfiguration(new CustomerEntityConfig());
        modelBuilder.ApplyConfiguration(new ReservationEntityConfig());
        modelBuilder.ApplyConfiguration(new ReservationItemEntityConfig());
    }
    
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        //TODO: Outbox pattern

        var response = await base.SaveChangesAsync(cancellationToken);
        await _dispatchDomainEvents();
        return response;
    }

    private async Task _dispatchDomainEvents()
    {
        var domainEventEntities = ChangeTracker.Entries<Entity>()
                                               .Select(po => po.Entity)
                                               .Where(po => po.DomainEvents.Any())
                                               .ToArray();

        foreach (var entity in domainEventEntities)
        {
            var events = entity.DomainEvents.ToArray();
            entity.DomainEvents.Clear();
            foreach (var entityDomainEvent in events)
            {
                await _mediator.Publish(entityDomainEvent);
            }
        }
    }
}