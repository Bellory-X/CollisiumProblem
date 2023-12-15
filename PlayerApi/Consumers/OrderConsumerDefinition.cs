using MassTransit;

namespace PlayerApi.Consumers;

public class OrderConsumerDefinition : ConsumerDefinition<OrderConsumer>
{
    public OrderConsumerDefinition()
    {
        EndpointName = "player";
    }
}