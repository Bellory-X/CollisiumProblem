using ColiseumLibrary.Model.Orders;
using MassTransit;

namespace OpponentApi.Consumers;

public class OpponentConsumer(ILogger<OpponentConsumer> logger/*PlayerService service*/) : IConsumer<Order>
{
    public async Task Consume(ConsumeContext<Order> context)
    {
        logger.LogInformation("Experiment id: {}, card number: {}", context.Message.Id, 17);
        await context.Publish(new OrderCreated{Id = context.Message.Id, Ordinal = 17, IsPlayer = false});
    }
}