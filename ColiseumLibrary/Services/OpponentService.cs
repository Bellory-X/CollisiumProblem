using ColiseumLibrary.Interfaces;
using ColiseumLibrary.Model.Orders;
using ColiseumLibrary.Strategies;
using Microsoft.Extensions.Logging;

namespace ColiseumLibrary.Services;

public class OpponentService(
    LastCardStrategy strategy,
    ILogger<PlayerService> logger
)
{
    public OrderCreated CreateOrder(Order order, bool isPlayer)
    {
        var cardNumber = strategy.Pick(order.Cards.ToArray());
        logger.LogInformation("Experiment id: {}, card number: {}", order.Id, cardNumber);
        return new OrderCreated{Id = order.Id, Ordinal = cardNumber, IsPlayer = isPlayer};
    }
}