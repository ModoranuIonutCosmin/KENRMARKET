using Customers.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Customers.Infrastructure.Data_Access.Config;


public class AddressEntityConfig : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder
                    .HasKey(a => a.Id);
        builder
                    .HasIndex(cd => cd.CustomerId);


        builder
                    .Property(a => a.AddressLine1)
                    .HasMaxLength(256);
        builder
                    .Property(a => a.AddressLine2)
                    .HasMaxLength(256);
        builder
                    .Property(a => a.Apartment)
                    .HasMaxLength(256);
        builder
                    .Property(a => a.City)
                    .HasMaxLength(256);
        builder
                    .Property(a => a.County)
                    .HasMaxLength(256);
        builder
                    .Property(a => a.PostalCode)
                    .HasMaxLength(256);
        builder
                    .Property(a => a.State)
                    .HasMaxLength(256);
    }
}
