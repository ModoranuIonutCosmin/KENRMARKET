using Customers.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Customers.Infrastructure.Data_Access.Config
{
    public class ReservationItemEntityConfig : IEntityTypeConfiguration<ReservationItem>
    {
        public void Configure(EntityTypeBuilder<ReservationItem> builder)
        {

        }
    }
}
