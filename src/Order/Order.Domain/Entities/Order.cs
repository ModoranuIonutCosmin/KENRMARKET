using Order.Domain.DataModels;
using Order.Domain.Models;
using Order.Domain.Shared;

namespace Order.Domain.Entities
{
    [Serializable]
    public class Order : Entity, IAggregateRoot
    {
        private Guid _buyerId;
        public Guid BuyerId => _buyerId;

        private List<OrderItem> _orderItems = new List<OrderItem>();
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;
        private string _promocode;
        public string Promocode => _promocode;

        public Address Address { get; set; } //TODO: Gasit cum se face Owned Entity sa fie Backed Field
        public decimal Total => OrderItems.Sum(ci => ci.Quantity * ci.UnitPrice);

        private DateTimeOffset _dateCreated;
        public DateTimeOffset DateCreated => _dateCreated;

        private OrderStatus _orderStatus;
        public OrderStatus OrderStatus => _orderStatus;

        protected Order()
        {

        }

        public Order(Guid buyerId, Address address)
        {
            this._buyerId = buyerId;
            this.Address = address;

            //TODO: Anuntat Payments ca se incepe order
            _orderStatus = OrderStatus.InitialCreation;
            _dateCreated = DateTimeOffset.UtcNow; 
        }

        public void AddOrderItem(Guid productId, string productName, decimal quantity, decimal price, string pictureUrl)
        {

            if (_orderItems.Any(oi => oi.ProductId.Equals(productId)))
            {
                OrderItem orderItem = _orderItems.Single(oi => oi.ProductId.Equals(productId));

                orderItem.AddToQuantity(quantity);
            }
            else
            {
                this._orderItems.Add(new OrderItem()
                {
                    ProductId = productId,
                    ProductName = productName,
                    Quantity = quantity,
                    UnitPrice = price,
                    PictureUrl = pictureUrl,
                });
            }
        }

        public void SetOrderStatus(OrderStatus orderStatus)
        {
            _orderStatus = orderStatus;
        }

        public void SetPromocode(string promocode)
        {
            this._promocode = promocode;
        }

    }
}

