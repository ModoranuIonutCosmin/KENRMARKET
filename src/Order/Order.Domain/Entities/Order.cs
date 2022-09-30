using Order.Domain.DataModels;
using Order.Domain.Models;
using Order.Domain.Shared;

namespace Order.Domain.Entities
{
    [Serializable]
    public class Order : Entity, IAggregateRoot
    {
        public Guid BuyerId { get; }
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;
        public string Promocode { get; }
        public Address Address { get; }
        public decimal Total => OrderItems.Sum(ci => ci.Quantity * ci.UnitPrice);
        public bool IsDraft => _isDraft;

        private bool _isDraft;
        public Guid PaymentMethodId { get;}
        private List<OrderItem> _orderItems { get; set; }

        private OrderStatus _orderStatus { get; set; }
        public OrderStatus OrderStatus => _orderStatus;

        protected Order()
        {

        }

        public Order(Guid buyerId, Address address, string cardUserName, string cardNumber,
            string cardSecurityNumber, DateTimeOffset cardExpirationDate, Guid? paymentMethodId)
        {
            this.BuyerId = buyerId;
            this.Address = address;
            this.PaymentMethodId = paymentMethodId ?? Guid.Empty;

            //TODO: Anuntat Payments ca se incepe order
        }

        public static Order GetBlankOrder()
            => new()
            {
                _isDraft = true
            };

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
                    PictureUrl = pictureUrl
                });
            }
        }

        public void SetOrderStatus(OrderStatus orderStatus)
        {
            _orderStatus = orderStatus;
        }

    }
}

