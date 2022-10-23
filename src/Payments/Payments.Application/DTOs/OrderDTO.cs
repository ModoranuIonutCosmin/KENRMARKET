using Payments.Domain.DataModels;
using Payments.Domain.Models;

namespace Payments.Application.DTOs
{
    public class OrderDTO
    {
        public Guid BuyerId { get; }
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
