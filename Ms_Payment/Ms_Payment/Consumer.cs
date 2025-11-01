using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using Contracts;
using MassTransit;

namespace Ms_Payment
{
    public class Consumer
    {
        public class PaymentRequestedConsumer : IConsumer<PaymentRequestedEvent>
        {
            private readonly ILogger<PaymentRequestedConsumer> _logger;
            public PaymentRequestedConsumer(ILogger<PaymentRequestedConsumer> logger)
            {
                _logger = logger;
            }
            public async Task Consume(ConsumeContext<PaymentRequestedEvent> context)
            {
                var message = context.Message;
                _logger.LogInformation($"Recebido pedido para pagamento: {message.OrderId} no valor de {message.TotalAmount}");
                // bool pagamentoAprovado = await _gateway.Processar(message.TotalAmount);
                // bool pagamentoAprovado = true;

                // if (pagamentoAprovado)
                // {
                //     await context.Publish(new PagamentoAprovadoEvent(message.OrderId));
                // }
                // else
                // {
                //     await context.Publish(new PagamentoRecusadoEvent(message.OrderId, "Pagamento recusado"));
                // }
            }
        }
    }
}
