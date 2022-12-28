using Cart.Domain.Entities;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Cart.Infrastructure.Data_Access;

public class CartsDBContext : DbContext
{
    public CartsDBContext()
    {
    }

    public CartsDBContext(DbContextOptions<CartsDBContext> options) : base(options)
    {
    }

    public DbSet<CartDetails> Carts     { get; set; }
    public DbSet<CartItem>    CartItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<CartDetails>()
                    .HasKey(cd => cd.Id);

        modelBuilder.Entity<CartDetails>()
                    .HasAlternateKey(cd => cd.CustomerId);

        modelBuilder.Entity<CartDetails>()
                    .Property(p => p.CustomerId)
                    .HasMaxLength(256);

        modelBuilder.Entity<CartDetails>()
                    .Property(p => p.Promocode)
                    .HasMaxLength(256);

        modelBuilder.Entity<CartItem>()
                    .HasKey(ci => ci.Id);

        modelBuilder.Entity<CartItem>()
                    .Property(p => p.ProductId)
                    .HasMaxLength(256);

        modelBuilder.Entity<CartItem>()
                    .Property(p => p.PictureUrl)
                    .HasMaxLength(2048);

        modelBuilder.Entity<CartItem>()
                    .Property(p => p.ProductName)
                    .HasMaxLength(512);


        modelBuilder.AddInboxStateEntity();
        modelBuilder.AddOutboxMessageEntity();
        modelBuilder.AddOutboxStateEntity();

        //Relations
        modelBuilder.Entity<CartDetails>()
                    .HasMany(c => c.CartItems)
                    .WithOne();
    }
}