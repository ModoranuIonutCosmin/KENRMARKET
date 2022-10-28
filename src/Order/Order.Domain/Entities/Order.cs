using Order.Domain.DataModels;
using Order.Domain.Models;
using Order.Domain.Shared;

namespace Order.Domain.Entities;

[Serializable]
public class Order : Entity, IAggregateRoot
{
    private List<OrderItem> _orderItems = new();
    private Guid _buyerId;
    private string _promocode;
    private OrderStatus _orderStatus;
    private DateTimeOffset _dateCreated;

    protected Order()
    {
    }

    public Order(Guid buyerId, Address address)
    {
        _buyerId = buyerId;
        Address = address;

        //TODO: Anuntat Payments ca se incepe order
        _orderStatus = OrderStatus.InitialCreation;
        _dateCreated = DateTimeOffset.UtcNow;
    }

    public Guid BuyerId
    {
        get
        {
            return _buyerId;
        }
    }

    public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;
    public string Promocode
    {
        get { return _promocode; }
    }

    public Address Address { get; set; } //TODO: Gasit cum se face Owned Entity sa fie Backed Field
    public decimal Total => OrderItems.Sum(ci => ci.Quantity * ci.UnitPrice);
    public DateTimeOffset DateCreated
    {
        get { return _dateCreated; }
    }

    public OrderStatus OrderStatus
    {
        get { return _orderStatus; }
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
        _orderStatus = orderStatus;
    }

    public void SetPromocode(string promocode)
    {
        _promocode = promocode;
    }
}