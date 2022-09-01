using Microsoft.EntityFrameworkCore;
using Cart.Domain.Entities;

namespace Cart.Infrastructure.Data_Access
{
    public class CartsDBContext : DbContext
    {
        public DbSet<CartDetails> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

        public CartsDBContext()
        {
            
        }
        public CartsDBContext(DbContextOptions<CartsDBContext> options): base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CartDetails>()
                .HasKey(cd => cd.CustomerId);

            modelBuilder.Entity<CartDetails>()
                .HasIndex(cd => cd.CustomerId);

            modelBuilder.Entity<CartItem>()
                .HasIndex(ci => ci.ProductId)
                .IsUnique();


            modelBuilder.Entity<CartDetails>()
                .Property(p => p.CustomerId)
                .HasMaxLength(256);

            modelBuilder.Entity<CartDetails>()
                .Property(p => p.Promocode)
                .HasMaxLength(256);



            modelBuilder.Entity<CartItem>()
                .Property(p => p.ProductId)
                .HasMaxLength(256);

            modelBuilder.Entity<CartItem>()
                .Property(p => p.PictureUrl)
                .HasMaxLength(2048); 

            modelBuilder.Entity<CartItem>()
                .Property(p => p.ProductName)
                .HasMaxLength(512);
            modelBuilder.Entity<CartItem>()
                .Property(p => p.CartCustomerId)
                .HasMaxLength(256);

            //Relations
            modelBuilder.Entity<CartDetails>()
                .HasMany<CartItem>(c => c.CartItems)
                .WithOne(ci => ci.CartDetails)
                .HasForeignKey(c => c.CartCustomerId);
        }
    }
}

