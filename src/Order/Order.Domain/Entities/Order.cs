using Order.Domain.DataModels;
using Order.Domain.DomainEvents;
using Order.Domain.Models;
using Order.Domain.Shared;

namespace Order.Domain.Entities;

[Serializable]
public class Order : Entity, IAggregateRoot
{
    private List<OrderItem> _orderItems = new();
    public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

    public Guid BuyerId { get; private set; }
    // private Guid _buyerId;

    public string Promocode { get; private set; }
    // private string _promocode;

    public Address Address { get; private set; } //TODO: Gasit cum se face Owned Entity sa fie Backed Field
    public DateTimeOffset DateCreated { get; private set; }
    // private DateTimeOffset _dateCreated;

    public OrderStatus OrderStatus { get; private set; }
    // private OrderStatus _orderStatus;

    public decimal Total => OrderItems.Sum(ci => ci.Quantity * ci.UnitPrice);
    
    
    protected Order()
    {
    }

    public Order(Guid buyerId, Address address)
    {
        BuyerId = buyerId;
        Address = address;
        
        //TODO: Anuntat Payments ca se incepe order
        
        this.AddDomainEvent(new OrderStartedDomainEvent(this));
        
        DateCreated = DateTimeOffset.UtcNow;
    }

    public void AddOrderItem(Guid productId, string productName, decimal quantity, decimal price, string pictureUrl)
    {
        if (_orderItems.Any(oi => oi.ProductId.Equals(productId)))
        {
            var orderItem = _orderItems.Single(oi => oi.ProductId.Equals(productId));

            orderItem.AddToQuantity(quantity);
        }
        else
        {
            _orderItems.Add(new OrderItem
            {
                ProductId = productId,
                ProductName = productName,
                Quantity = quantity,
                UnitPrice = price,
                PictureUrl = pictureUrl
            });
        }
    }

    public void SetOrderStatus(OrderStatus orderStatus)
    {
        OrderStatus = orderStatus;

        switch (orderStatus)
        {
            case OrderStatus.InitialCreation:
                break;
            case OrderStatus.PendingValidation:

                var domainEvent = new OrderStatusChangedToPendingValidationDomainEvent(this, BuyerId);
                
                this.AddDomainEvent(domainEvent);
                break;
            case OrderStatus.Paid:
                
                //TODO: Schimbat din command aici setarea
                break;
        }
    }

    public void SetPromocode(string promocode)
    {
        Promocode = promocode;
    }
}