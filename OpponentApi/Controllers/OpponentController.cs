using ColiseumLibrary.Contracts.Orders;
using ColiseumLibrary.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace OpponentApi.Controllers;

[ApiController]
[Route("opponent")]
public class OpponentController(
    ICardPickStrategy strategy,
    ILogger<OpponentController> logger
) : ControllerBase
{
    [HttpPost]
    public OrderCreated CreateOrder(Order order)
    {
        var cardNumber = strategy.Pick(order.Cards.ToArray());
        logger.LogInformation("Experiment id: {}, card number: {}", order.Id, cardNumber);
        return new OrderCreated {Id = order.Id, CardNumber = cardNumber };
    }
}
