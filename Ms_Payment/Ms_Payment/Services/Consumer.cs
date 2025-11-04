using Contracts;
using MassTransit;
using Contracts.Enums;
using Ms_Payment.Entity;
using Ms_Payment.Interfaces;

namespace Ms_Payment
{
    public class Consumer
    {
        public class PaymentRequestedConsumer : IConsumer<PaymentRequestedEvent>
        {
            private readonly ILogger<PaymentRequestedConsumer> _logger;
            private readonly IPaymentRepository _repository;

            public PaymentRequestedConsumer(ILogger<PaymentRequestedConsumer> logger, IPaymentRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }
            public async Task Consume(ConsumeContext<PaymentRequestedEvent> context)
            {
                var message = context.Message;
                _logger.LogInformation($"Recebido pedido para pagamento: {message.OrderId} no valor de {message.TotalAmount}");

                var transaction = new OrderTransaction
                {
                    Id = Guid.NewGuid(),
                    OrderId = message.OrderId,
                    TotalAmount = message.TotalAmount,
                    CreatedAt = DateTime.UtcNow,
                    Status = Status.Pending
                };

                await _repository.Create(transaction);

                bool paymentApproved = true;

                if (paymentApproved)
                {
                    transaction.Status = Status.Approved;
                    await context.Publish(new PaymentApprovedEvent(message.OrderId));
                }
                else
                {
                    transaction.Status = Status.Refused;
                    await context.Publish(new PaymentRefusedEvent(message.OrderId, "Pagamento recusado"));
                }
                await _repository.Update(transaction, transaction.Status);
            }
        }
    }
}
