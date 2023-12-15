using MassTransit;

namespace OpponentApi.Consumers;

public class OrderConsumerDefinition : ConsumerDefinition<OrderConsumer>
{
    public OrderConsumerDefinition()
    {
        EndpointName = "opponent";
    }
}