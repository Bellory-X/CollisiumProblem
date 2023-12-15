using ColiseumLibrary.Contracts.Orders;
using ColiseumLibrary.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace PlayerApi.Controllers;

[ApiController]
[Route("player")]
public class PlayerController(
    ICardPickStrategy strategy,
    ILogger<PlayerController> logger
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
