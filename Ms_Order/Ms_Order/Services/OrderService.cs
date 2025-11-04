using System.Security.Claims;
using Ms_Order.DTOs;
using Ms_Order.Entities;
using Ms_Order.Interfaces;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using MassTransit;
using Contracts;
using Contracts.Enums;
using AutoMapper;
using System.Transactions;

namespace Ms_Order.Services
{

    public class OrderService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IOrderRepository _orderRepository;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IMapper _mapper;
        public OrderService(IHttpClientFactory httpClientFactory,
                            IHttpContextAccessor httpContextAccessor,
                            IOrderRepository orderRepository, IPublishEndpoint publishEndpoint, IMapper mapper)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
            _orderRepository = orderRepository;
            _publishEndpoint = publishEndpoint;
            _mapper = mapper;
        }
        public async Task<Order> Create(CreateOrderDTO orderRequest)
        {
            var token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();
            var productClient = _httpClientFactory.CreateClient("Product");
            productClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", token);

            var userClaims = _httpContextAccessor.HttpContext.User;

            var userId = userClaims.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                throw new Exception("Token do usuário está incompleto.");
            }

            var newOrder = new Order
            {
                OrderId = Guid.NewGuid(),
                UserId = Guid.Parse(userId),
                Status = "Waiting payment",
                CreatedAt = DateTime.UtcNow,
                OrderProducts = new List<OrderProducts>()
            };
            double productAmount = 0;
            

            foreach (var products in orderRequest.Products)
            {
                decimal price;
                var stockValidation = new CreateOrderDTO
                {
                    Products = new List<CreateOrderProductDTO>
                    {
                        new CreateOrderProductDTO
                        {
                            ProductId = products.ProductId,
                            Quantity = products.Quantity
                        }
                    }
                };
                var response = await productClient.PostAsJsonAsync("/product/stock", stockValidation);
                if (response == null || !response.IsSuccessStatusCode)
                {
                    throw new Exception($"Produto com ID {products.ProductId} não encontrado.");
                }

                var validationResult = await response.Content.ReadFromJsonAsync<JsonElement>();

                if (validationResult.ValueKind == JsonValueKind.Array)
                {
                    var firstProduct = validationResult[0];
                    price = firstProduct.GetProperty("price").GetDecimal();

                    productAmount += (double)(products.Quantity * price);
                }
                else
                {
                    price = validationResult.GetProperty("price").GetDecimal();
                    productAmount +=(double)(products.Quantity * price);
                }
                var orderProductItem = new OrderProducts
                {
                    OrderProuctsId = Guid.NewGuid(),
                    ProductId = products.ProductId,
                    Price = price
                };
                newOrder.OrderProducts.Add(orderProductItem);
            }
            newOrder.OrderId = Guid.NewGuid();
            newOrder.UserId = Guid.Parse(userId);
            newOrder.TotalAmount = productAmount;
            newOrder.Status = Status.Pending.ToString();
            newOrder.CreatedAt = DateTime.UtcNow;

            await _orderRepository.Create(newOrder);

            await _publishEndpoint.Publish(new PaymentRequestedEvent(
             newOrder.OrderId,
             newOrder.TotalAmount,
             Status.Pending
         ));
            return newOrder;
        }
    }
}