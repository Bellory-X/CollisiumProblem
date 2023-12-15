using ColiseumLibrary.Model.Orders;
using ColiseumLibrary.Workers;
using Microsoft.AspNetCore.Mvc;

namespace OpponentApi.Controllers;

[ApiController]
[Route("opponent")]
public class OpponentController(OrderService service) : ControllerBase
{
    [HttpPost]
    public OrderCreated CreateOrder(Order order) => service.CreateOrder(order);
}
