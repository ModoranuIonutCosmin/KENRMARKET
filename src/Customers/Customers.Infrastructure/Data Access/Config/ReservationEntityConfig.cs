using Customers.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Customers.Infrastructure.Data_Access.Config
{
    public class ReservationEntityConfig : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {

            builder.HasMany<ReservationItem>(r => r.ReservationItems)
                .WithOne();
        }
    }
}
