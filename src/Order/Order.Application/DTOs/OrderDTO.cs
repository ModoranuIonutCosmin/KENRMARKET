using Order.Domain.DataModels;
using Order.Domain.Entities;

namespace Order.Application.DTOs
{
    public class OrderDTO
    {
        public Guid Id { get; set; }
        public Guid BuyerId { get; set; }
        public IReadOnlyCollection<OrderItem> OrderItems
        {
            get;
            init;
        }
        public string Promocode { get; }
        public OrderStatus OrderStatus { get; init; }
        public decimal Total => OrderItems.Sum(ci => ci.Quantity * ci.UnitPrice);
    }
}
