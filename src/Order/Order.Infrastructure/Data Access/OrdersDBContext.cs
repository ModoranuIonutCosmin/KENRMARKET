using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Order.Domain.Entities;
using Order.Domain.Shared;
using Order.Infrastructure.EntitiesConfig;

namespace Order.Infrastructure.Data_Access;

public class OrdersDBContext : DbContext
{
    private readonly IMediator _mediator;

    public OrdersDBContext(DbContextOptions<OrdersDBContext> options, IMediator mediator) : base(options)
    {
        _mediator = mediator;
    }

    public DbSet<Domain.Entities.Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new OrderEntityConfiguration());
        modelBuilder.AddInboxStateEntity();
        modelBuilder.AddOutboxMessageEntity();
        modelBuilder.AddOutboxStateEntity();
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
                await _mediator.Publish(entityDomainEvent);
        }
    }
}