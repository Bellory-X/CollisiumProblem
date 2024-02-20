using ColiseumLibrary.Model.Cards;
using ColiseumLibrary.Model.Orders;
using ColiseumLibrary.Services;
using Microsoft.AspNetCore.Mvc;

namespace PlayerApi.Controllers;

[ApiController]
[Route("player")]
public class PlayerController(PlayerService service) : ControllerBase
{
    // [HttpPost]
    // public OrderCreated CreateOrder(Order order) => service.CreateOrder(order, true);
    //
    // [HttpGet("{id:int}/{ordinal:int}")]
    // public Card GetCard(int id, int ordinal) => service.GetCard(id, ordinal);
}
