using MassTransit;
using Microsoft.EntityFrameworkCore;
using Payments.Domain.Entities;
using Payments.Infrastructure.EntitiesConfig;

namespace Payments.Infrastructure.Data_Access
{
    public class PaymentsDBContext : DbContext
    {
        public DbSet<Payment> Payments { get; set; }

        public PaymentsDBContext()
        {
            
        }
        public PaymentsDBContext(DbContextOptions<PaymentsDBContext> options): base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new PaymentEntityConfiguration());

            modelBuilder.AddInboxStateEntity();
            modelBuilder.AddOutboxMessageEntity();
            modelBuilder.AddOutboxStateEntity();
        }
    }
}

