using MassTransit;

namespace PlayerApi.Consumers;

public class PlayerConsumerDefinition : ConsumerDefinition<PlayerConsumer>
{
    public PlayerConsumerDefinition()
    {
        EndpointName = "player";
    }
}