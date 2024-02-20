using ColiseumLibrary.Model.Orders;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace GodsApi.Consumers;

public class GodsConsumer(/*MassTransitExperimentService service*/) : IConsumer<OrderCreated>
{
    public async Task Consume(ConsumeContext<OrderCreated> context)
    {
        // service.Run(context.Message);
        Console.WriteLine("Add choice: id: " +  context.Message.Id + ", value: "+ context.Message.Ordinal +", isPlayer: " + context.Message.IsPlayer);
    }
}