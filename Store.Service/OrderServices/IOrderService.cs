

using Store.data.Entity;
using Store.Service.OrderServices.Dto;

namespace Store.Service.OrderServices
{
    public interface IOrderService
    {
        Task<OrderDetailsDto> CreateOrderAsync(OrderDto input);
        Task<IReadOnlyList<OrderDetailsDto>> GetUSerOrdersAsync(string buyerEmail);
        Task<OrderDetailsDto> GetOrderByIdAsync(Guid id);
        Task<IReadOnlyList<DeliveryMethod>> GetAllDeliveryMethodAsync();
    }
}
