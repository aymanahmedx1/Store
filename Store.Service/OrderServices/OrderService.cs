
using AutoMapper;

using Store.data.Entity;
using Store.data.Entity.OrderEntity;
using Store.Repository.Interface;
using Store.Repository.Specification.OrderSpecification;
using Store.Service.BasketService;
using Store.Service.OrderServices.Dto;

namespace Store.Service.OrderServices
{
    public class OrderService(IBasketSrevice _basketService, IUnitOfWork _unitOfWork, IMapper _mapper) : IOrderService
    {
        public async Task<OrderDetailsDto> CreateOrderAsync(OrderDto input)
        {
            var basket = await _basketService.GetBasketAsync(input.BasketId);
            if (basket == null)
                throw new Exception("Basket Not Found");
            #region Fill Order Item List From Basket 
            var orderItems = new List<OrderItemDto>();
            foreach (var item in basket.BasketItems)
            {
                var product = await _unitOfWork.Repository<Product, int>().GetByIdAsync(item.ProductId);
                if (product == null)
                    throw new Exception($"product {item.ProductId} Not Found");
                var productItem = new ProductItem
                {
                    ProductId = product.Id,
                    ImageUrl = product.PictureUrl,
                    ProductName = product.Name,
                };
                var orderItem = new OrderItem
                {
                    price = product.Price,
                    Quantity = item.Quantity,
                    ProductItem = productItem
                };
                var mappedOrderItem = _mapper.Map<OrderItemDto>(orderItem);
            }
            #endregion

            #region Delivery Method
            var deliverMethod = _unitOfWork.Repository<DeliveryMethod, int>().GetByIdAsync(basket.DeliveryMethodId);
            if (deliverMethod == null)
                throw new Exception("No Deliver Method");
            #endregion
            #region CalcSubTotal
            var subTotal = orderItems.Sum(x => x.Quantity * x.price);
            #endregion

            #region Order
            var shippingAddress = _mapper.Map<ShippingAddress>(input.ShippingAddress);
            var mappedOrderItems = _mapper.Map<List<OrderItem>>(orderItems);
            var order = new Order
            {
                DeliveryMethodId = deliverMethod.Id,
                ShippingAddress = shippingAddress,
                BuyerEmail = input.BuyerEmail,
                BasketId = input.BasketId,
                OrderItems = mappedOrderItems,
                SubTotal = subTotal,
            };
            await _unitOfWork.Repository<Order, Guid>().AddAsync(order);
            await _unitOfWork.CompleteAsync();
            var mappedOrder = _mapper.Map<OrderDetailsDto>(order);
            return mappedOrder;
            #endregion 
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetAllDeliveryMethodAsync()
         => await _unitOfWork.Repository<DeliveryMethod, int>().GetAllAsync();

        public async Task<OrderDetailsDto> GetOrderByIdAsync(Guid id)
        {
            var specs = new OrderWithItemSpecification(id);
            var order = await _unitOfWork.Repository<Order, Guid>().GetWithSpecificationByIdAsync(specs);
            if (order is null)
                throw new Exception($"Can not find Order With Id {id} !");
            var mappedOrder = _mapper.Map<OrderDetailsDto>(order);
            return mappedOrder;
        }

        public async Task<IReadOnlyList<OrderDetailsDto>> GetUSerOrdersAsync(string buyerEmail)
        {
            var specs = new OrderWithItemSpecification(buyerEmail);
            var orders = await _unitOfWork.Repository<Order, Guid>().GetAllWithSpecificationAsync(specs);
            if (!orders.Any())
                throw new Exception("There is no Orders found !"); 
            var mappedOrders = _mapper.Map<List<OrderDetailsDto>>(orders);
            return mappedOrders; 
        }
    }
}
