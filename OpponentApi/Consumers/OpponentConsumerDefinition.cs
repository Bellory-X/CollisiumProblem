using MassTransit;

namespace OpponentApi.Consumers;

public class OpponentConsumerDefinition : ConsumerDefinition<OpponentConsumer>
{
    public OpponentConsumerDefinition()
    {
        EndpointName = "opponent";
    }
}