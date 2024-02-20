using ColiseumLibrary.Model.Cards;
using ColiseumLibrary.Model.Orders;
using ColiseumLibrary.Services;
using Microsoft.AspNetCore.Mvc;

namespace OpponentApi.Controllers;

[ApiController]
[Route("opponent")]
public class OpponentController(PlayerService service) : ControllerBase
{
    // [HttpPost]
    // public OrderCreated CreateOrder(Order order) => service.CreateOrder(order, false);
    //
    // [HttpGet("{id:int}/{ordinal:int}")]
    // public Card GetCardColor(int id, int ordinal) => service.GetCard(id, ordinal);
}
