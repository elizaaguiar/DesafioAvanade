using System.Security.Claims;
using Ms_Order.DTOs;
using Ms_Order.Entities;
using Ms_Order.Interfaces;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using MassTransit;
using Contracts;

namespace Ms_Order.Services
{

    public class OrderService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IOrderRepository _orderRepository;
        private readonly IPublishEndpoint _publishEndpoint;
        public OrderService(IHttpClientFactory httpClientFactory,
                            IHttpContextAccessor httpContextAccessor,
                            IOrderRepository orderRepository, IPublishEndpoint publishEndpoint)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
            _orderRepository = orderRepository;
            _publishEndpoint = publishEndpoint;
        }
        public async Task<Order> Create(CreateOrderDTO orderRequest)
        {
            var token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();
            var productClient = _httpClientFactory.CreateClient("Product");
            productClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", token);

            var product = await productClient.GetFromJsonAsync<ProductDTO>($"/product/{orderRequest.}");

            var userClaims = _httpContextAccessor.HttpContext.User;

            var userId = userClaims.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = userClaims.FindFirstValue(ClaimTypes.Email);

            if (string.IsNullOrEmpty(userId))
            {
                throw new Exception("Token do usuário está incompleto.");
            }

            if (product == null)
            {
                throw new Exception($"Produto com ID {orderRequest.ProductId} não encontrado.");
            }

            var newOrder = new Order
            {
                OrderId = Guid.NewGuid(),
                UserId = Guid.Parse(userId),
                ProductId = orderRequest.ProductId,
                TotalAmount = (double)(orderRequest.Quantity * product.Amount),
                Status = "WaitingPayment",
                CreatedAt = DateTime.UtcNow
            };
            await _orderRepository.Create(newOrder);

            await _publishEndpoint.Publish(new PaymentRequestedEvent(
             newOrder.OrderId,
             newOrder.TotalAmount,
             newOrder.Status
         ));
            return newOrder;
        }
    }
}