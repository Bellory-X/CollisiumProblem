using ColiseumLibrary.Contracts.Orders;
using ColiseumLibrary.Interfaces;
using MassTransit;

namespace OpponentApi.Consumers;

public class OrderConsumer(ICardPickStrategy strategy, ILogger<OrderConsumer> logger) : IConsumer<Order>
{
    public Task Consume(ConsumeContext<Order> context)
    {
        var order = context.Message;
        var cardNumber = strategy.Pick(order.Cards.ToArray());
        logger.LogInformation("Experiment id: {}, card number: {}", order.Id, cardNumber);
        return context.Publish<OrderCreated>(new { order.Id, cardNumber });
    }
}