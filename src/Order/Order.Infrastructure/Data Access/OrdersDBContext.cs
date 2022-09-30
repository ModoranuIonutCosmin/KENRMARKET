using Microsoft.EntityFrameworkCore;
using Order.Domain.Entities;

namespace Order.Infrastructure.Data_Access
{
    public class OrdersDBContext : DbContext
    {
        public DbSet<Domain.Entities.Order> Carts { get; set; }
        public DbSet<OrderItem> CartItems { get; set; }

        public OrdersDBContext()
        {
            
        }
        public OrdersDBContext(DbContextOptions<OrdersDBContext> options): base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Domain.Entities.Order>()
                .HasKey(cd => cd.BuyerId);

            modelBuilder.Entity<Domain.Entities.Order>()
                .HasIndex(cd => cd.BuyerId);

            modelBuilder.Entity<OrderItem>()
                .HasIndex(ci => ci.ProductId)
                .IsUnique();


            modelBuilder.Entity<Domain.Entities.Order>()
                .Property(p => p.BuyerId)
                .HasMaxLength(256);

            modelBuilder.Entity<Domain.Entities.Order>()
                .Property(p => p.Promocode)
                .HasMaxLength(256);



            modelBuilder.Entity<OrderItem>()
                .Property(p => p.ProductId)
                .HasMaxLength(256);

            modelBuilder.Entity<OrderItem>()
                .Property(p => p.PictureUrl)
                .HasMaxLength(2048); 

            modelBuilder.Entity<OrderItem>()
                .Property(p => p.ProductName)
                .HasMaxLength(512);
            modelBuilder.Entity<OrderItem>()
                .Property(p => p.CartCustomerId)
                .HasMaxLength(256);

            //Relations
            modelBuilder.Entity<Domain.Entities.Order>()
                .HasMany<OrderItem>(c => c.OrderItems)
                .WithOne()
                .HasForeignKey(c => c.CartCustomerId);
        }
    }
}

