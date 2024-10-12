using AutoMapper;

using Microsoft.Extensions.Configuration;

using Store.data.Entity;
using Store.data.Entity.OrderEntity;
using Store.Repository.Interface;
using Store.Repository.Specification.OrderSpecification;
using Store.Service.BasketService;
using Store.Service.BasketService.Dtos;
using Store.Service.OrderServices.Dto;

using Stripe;

namespace Store.Service.PaymentServices
{
    public class PaymentService(IConfiguration _config, IUnitOfWork _unitOfWork, IBasketSrevice _basketService , IMapper _mapper) : IPaymentService
    {
        public async Task<CustomerBasketDto> CreateOrUpdatePaymentIntent(CustomerBasketDto input)
        {
            if (input is null)
                throw new Exception("Basket Is Empty");
            var deliverMethod = await _unitOfWork.Repository<DeliveryMethod, int>().GetByIdAsync(input.DeliveryMethodId);
            if (deliverMethod == null)
                throw new Exception("No Deliver Method");
            StripeConfiguration.ApiKey = _config["Strip:SecretKey"];
            var shippingPrice = deliverMethod.Price;
            foreach (var item in input.BasketItems)
            {
                var product = await _unitOfWork.Repository<data.Entity.Product, int>().GetByIdAsync(item.ProductId);
                if (item.Price != product.Price)
                    item.Price = product.Price;
            }
            var payment = new PaymentIntentService();
            PaymentIntent paymentIntent;
            if (string.IsNullOrEmpty(input.PaymentIntentId))
            {
                var intentOption = new PaymentIntentCreateOptions
                {
                    Amount =(long) input.BasketItems.Sum(x => x.Quantity * (x.Price * 100)) + (long)(shippingPrice * 100),
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card" }
                };
                paymentIntent = await payment.CreateAsync(intentOption);
                input.PaymentIntentId = paymentIntent.Id;
                input.ClientSecret = paymentIntent.ClientSecret;
            }
            else
            {
                var updateIntentOption = new PaymentIntentUpdateOptions
                {
                    Amount = (long)input.BasketItems.Sum(x => x.Quantity * (x.Price * 100)) + (long)(shippingPrice * 100),
                };
                paymentIntent = await payment.UpdateAsync(input.PaymentIntentId, updateIntentOption);

            }
            await _basketService.UpdateBasketAsync(input);
            return input;
        }

        public async Task<OrderDetailsDto> UpdateOrderPaymentFailed(string paymentIntentId)
        {

            var specs = new OrderWithIntentIdSpecification(paymentIntentId);
            var order = await _unitOfWork.Repository<Order, Guid>().GetWithSpecificationByIdAsync(specs);
            if (order is null)
                throw new Exception("Order Not Found !");

            order.OrderPaymentStatus = OrderPaymentStatus.Failed;
            _unitOfWork.Repository<Order, Guid>().Update(order);
            await _unitOfWork.CompleteAsync();
            var mappedOrder = _mapper.Map<OrderDetailsDto>(order);
            return mappedOrder; 
        }

        public async Task<OrderDetailsDto> UpdateOrderPaymentSucceded(string paymentIntentId)
        {
            var specs = new OrderWithIntentIdSpecification(paymentIntentId);
            var order = await _unitOfWork.Repository<Order, Guid>().GetWithSpecificationByIdAsync(specs);
            if (order is null)
                throw new Exception("Order Not Found !");

            order.OrderPaymentStatus = OrderPaymentStatus.Received;
            _unitOfWork.Repository<Order, Guid>().Update(order);
            await _unitOfWork.CompleteAsync();
            await _basketService.DeleteBasketAsync(order.BasketId);
            var mappedOrder = _mapper.Map<OrderDetailsDto>(order);
            return mappedOrder;
        }
    }
}
