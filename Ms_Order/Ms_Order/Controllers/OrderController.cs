using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Ms_Order.DTOs;
using Ms_Order.Services;

namespace Ms_Order.Controllers
{
    [ApiController]
    [Route("order")]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;
        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDTO orderDTO)
        {
            var newOrder = await _orderService.Create(orderDTO);

            return Ok(newOrder);
        }
    }
}