using ColiseumLibrary.Model.Orders;
using ColiseumLibrary.Workers;
using Microsoft.AspNetCore.Mvc;

namespace PlayerApi.Controllers;

[ApiController]
[Route("player")]
public class PlayerController(OrderService service) : ControllerBase
{
    [HttpPost]
    public OrderCreated CreateOrder(Order order) => service.CreateOrder(order);
}
