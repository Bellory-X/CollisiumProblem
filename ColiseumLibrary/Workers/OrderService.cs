using ColiseumLibrary.Interfaces;
using ColiseumLibrary.Model.Orders;
using Microsoft.Extensions.Logging;

namespace ColiseumLibrary.Workers;

public class OrderService(ICardPickStrategy strategy, ILogger<OrderService> logger)
{
    public OrderCreated CreateOrder(Order order)
    {
        var cardNumber = strategy.Pick(order.Cards.ToArray());
        logger.LogInformation("Experiment id: {}, card number: {}", order.Id, cardNumber);
        return new OrderCreated {Id = order.Id, CardNumber = cardNumber };
    }
}