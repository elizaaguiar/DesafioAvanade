using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contracts
{
    public record PaymentRequestedEvent(Guid OrderId, double TotalAmount, string Status);
    public record OrderCreatedEvent
    {
        public Guid OrderId { get; init; }
        public Guid UserId { get; init; }
        public decimal TotalAmount { get; init; }
        public List<OrderItemContract> Items { get; init; }
    }
    public record OrderItemContract
    {
        public Guid ProductId { get; init; }
        public int Quantity { get; init; }
        public decimal Amount { get; init; }
    }
}