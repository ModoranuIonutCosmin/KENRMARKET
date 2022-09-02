using Customers.Domain.Entities;
using Customers.Infrastructure.Seed;
using Microsoft.EntityFrameworkCore;

namespace Customers.Infrastructure.Data_Access
{
    public class CustomersDBContext : DbContext
    {

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public CustomersDBContext()
        {
            
        }
        public CustomersDBContext(DbContextOptions<CustomersDBContext> options): base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>()
                .HasKey(c => c.Id);
            modelBuilder.Entity<Address>()
                .HasKey(a => a.Id);

            //Indexes


            modelBuilder.Entity<Address>()
                .HasIndex(cd => cd.CustomerId);

            modelBuilder.Entity<Customer>()
                .HasIndex(ci => ci.Email);

            modelBuilder.Entity<Customer>()
                .HasIndex(ci => ci.PhoneNumber);


            modelBuilder.Entity<Customer>()
                .Property(p => p.FirstName)
                .HasMaxLength(256);
            modelBuilder.Entity<Customer>()
                .Property(p => p.LastName)
                .HasMaxLength(256);
            modelBuilder.Entity<Customer>()
                .Property(p => p.MiddleName)
                .HasMaxLength(256);
            modelBuilder.Entity<Customer>()
                .Property(p => p.Email)
                .HasMaxLength(256);
            modelBuilder.Entity<Customer>()
                .Property(p => p.PhoneNumber)
                .HasMaxLength(256);



            modelBuilder.Entity<Address>()
                .Property(a => a.AddressLine1)
                .HasMaxLength(256);
            modelBuilder.Entity<Address>()
                .Property(a => a.AddressLine2)
                .HasMaxLength(256);
            modelBuilder.Entity<Address>()
                .Property(a => a.Apartment)
                .HasMaxLength(256);
            modelBuilder.Entity<Address>()
                .Property(a => a.City)
                .HasMaxLength(256);
            modelBuilder.Entity<Address>()
                .Property(a => a.County)
                .HasMaxLength(256);
            modelBuilder.Entity<Address>()
                .Property(a => a.PostalCode)
                .HasMaxLength(256);
            modelBuilder.Entity<Address>()
                .Property(a => a.State)
                .HasMaxLength(256);

            //Relations
            modelBuilder.Entity<Customer>()
                .HasOne<Address>(c => c.Address)
                .WithOne();




        }
    }
}

