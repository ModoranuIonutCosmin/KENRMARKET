using Gateway.Domain.Entities;
using Gateway.Infrastructure.Data_Access.Config;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Gateway.Infrastructure.Data_Access;

public class AuthenticationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
{
    public AuthenticationDbContext()
    {
    }

    public AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        modelBuilder.ApplyConfiguration(new ApplicationUserEntityConfig());
    }
}