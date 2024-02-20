using ColiseumLibrary.Model.Orders;
using ColiseumLibrary.Services;
using MassTransit;

namespace PlayerApi.Consumers;

public class PlayerConsumer(/*ILogger<PlayerConsumer> logger, PlayerService service*/) : IConsumer<Order>
{
    public async Task Consume(ConsumeContext<Order> context)
    {
        Console.WriteLine("Experiment id: " + context.Message.Id + ", card number: " + 0);
        // await context.Publish(new OrderCreated(context.Message.Id, 0, true));
        await context.Publish<OrderCreated>(new OrderCreated{Id = context.Message.Id, Ordinal = 0, IsPlayer = true});
    }
}