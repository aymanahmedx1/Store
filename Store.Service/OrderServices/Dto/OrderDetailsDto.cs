using Store.data.Entity.OrderEntity;
using Store.data.Entity;

namespace Store.Service.OrderServices.Dto
{
    public class OrderDetailsDto
    {
        public Guid Id { get; set; }
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public ShippingAddress ShippingAddress { get; set; }
        public string DeliveryMethodName { get; set; }
        public OrderPaymentStatus OrderPaymentStatus { get; set; } = OrderPaymentStatus.Pending;
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Placed;
        public IReadOnlyList<OrderItemDto> OrderItems { get; set; }
        public decimal SubTotal { get; set; }
        public decimal ShippngPrice { get; set; }
        public decimal Total { get; set; }
        public string BasketId { get; set; }
        public string? PaymentIntentId { get; set; }
    }
}
