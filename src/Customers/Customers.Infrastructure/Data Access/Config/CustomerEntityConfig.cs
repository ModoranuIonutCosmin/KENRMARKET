using Customers.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Customers.Infrastructure.Data_Access.Config
{
    public class CustomerEntityConfig : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            //Keys
            builder.HasKey(c => c.Id);


            //Indexes

            builder
            .HasIndex(ci => ci.Email);
            builder
                        .HasIndex(ci => ci.PhoneNumber);



            builder
                        .Property(p => p.FirstName)
                        .HasMaxLength(256);
            builder
                        .Property(p => p.LastName)
                        .HasMaxLength(256);
            builder
                        .Property(p => p.MiddleName)
                        .HasMaxLength(256);
            builder
                .Property(p => p.UserName)
                .HasMaxLength(256);
            builder
                        .Property(p => p.Email)
                        .HasMaxLength(256);
            builder
                        .Property(p => p.PhoneNumber)
                        .HasMaxLength(256);




            //Relations
            builder
                        .HasOne(c => c.Address)
                        .WithOne();

            builder.HasOne(c => c.Reservation)
                   .WithOne();
        }
    }
}
