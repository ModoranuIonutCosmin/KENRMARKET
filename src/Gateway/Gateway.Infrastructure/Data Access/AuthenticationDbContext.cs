using Gateway.Domain.Entities;
using Gateway.Domain.Shared;
using Gateway.Infrastructure.Data_Access.Config;
using MediatR;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Gateway.Infrastructure.Data_Access;

public class AuthenticationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
{
    private readonly IMediator _mediator;

    public AuthenticationDbContext()
    {
    }

    public AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> options, IMediator mediator) : base(options)
    {
        _mediator = mediator;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        modelBuilder.ApplyConfiguration(new ApplicationUserEntityConfig());
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
        var domainEventEntities = ChangeTracker.Entries<IEntity>()
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