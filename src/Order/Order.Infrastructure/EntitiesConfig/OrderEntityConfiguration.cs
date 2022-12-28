using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Domain.Models;

namespace Order.Infrastructure.EntitiesConfig;

public class OrderEntityConfiguration : IEntityTypeConfiguration<Domain.Entities.Order>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Order> orderConfiguration)
    {
        orderConfiguration
            .HasKey(o => o.Id);

        //Private fields

        // orderConfiguration
        //     .Property<Guid>("_buyerId")
        //     .UsePropertyAccessMode(PropertyAccessMode.Field)
        //     .HasColumnName("BuyerId")
        //     .IsRequired();
        //
        // orderConfiguration
        //     .Property<string>("_promocode")
        //     .UsePropertyAccessMode(PropertyAccessMode.Field)
        //     .HasColumnName("Promocode")
        //     .IsRequired(false);
        //
        // orderConfiguration
        //     .Property<DateTimeOffset>("_dateCreated")
        //     .UsePropertyAccessMode(PropertyAccessMode.Field)
        //     .HasColumnName("DateCreated")
        //     .IsRequired();
        //
        // orderConfiguration
        //     .Property<OrderStatus>("_orderStatus")
        //     .UsePropertyAccessMode(PropertyAccessMode.Field)
        //     .HasColumnName("OrderStatus")
        //     .IsRequired();
        //
        //
        //
        // orderConfiguration
        //     .Property("_buyerId")
        //     .HasMaxLength(256);
        //
        // orderConfiguration
        //     .Property("_promocode")
        //     .HasMaxLength(256);

        orderConfiguration
            .Property(o => o.BuyerId)
            .IsRequired();

        orderConfiguration
            .Property<string>(o => o.Promocode)
            .HasMaxLength(256)
            .IsRequired(false);

        orderConfiguration
            .Property(o => o.DateCreated)
            .IsRequired();

        orderConfiguration
            .Property(o => o.OrderStatus)
            .IsRequired();


        orderConfiguration
            .HasMany(o => o.OrderItems)
            .WithOne();

        orderConfiguration.Metadata.FindNavigation("OrderItems")
                          .SetPropertyAccessMode(PropertyAccessMode.Field);

        orderConfiguration
            .OwnsOne<Address>("Address", a =>
            {
                a.WithOwner();

                a.Property(p => p.AddressLine1)
                 .HasMaxLength(512);
                a.Property(p => p.AddressLine2)
                 .HasMaxLength(512)
                 .IsRequired(false);
                a.Property(p => p.Apartment)
                 .HasMaxLength(512);
                a.Property(p => p.City)
                 .HasMaxLength(512);
                a.Property(p => p.County)
                 .HasMaxLength(512);
                a.Property(p => p.PostalCode)
                 .HasMaxLength(512);
                a.Property(p => p.State)
                 .HasMaxLength(512);
            })
            ;
    }
}