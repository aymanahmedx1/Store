using System.Security.Claims;

using AutoMapper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Store.data.Entity;
using Store.Service.HandelReponse;
using Store.Service.OrderServices;
using Store.Service.OrderServices.Dto;

namespace Store.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrdersController(IOrderService _orderService, IMapper _mapper) : ControllerBase
    {

        [HttpPost]
        public async Task<ActionResult<OrderDetailsDto>> CreateOrderAsync(OrderDto input)
        {
            var order = await _orderService.CreateOrderAsync(input);
            if (order is null)
            {
                return BadRequest(new Response(400, "Error while Create Order"));
            }
            return Ok(order);
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderDetailsDto>>> GetUserOrders()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var orders = await _orderService.GetUSerOrdersAsync(email);
            return Ok(orders);
        }
        [HttpGet]
        public async Task<ActionResult<OrderDetailsDto>> GetOrderByIdAsync(Guid id)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var order = await _orderService.GetOrderByIdAsync(id);
            return Ok(order);
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetAllDeliveryMethodAsync(Guid id)
        {
            var deliveryMethods = await _orderService.GetAllDeliveryMethodAsync();
            return Ok(deliveryMethods);
        }
    }
}
