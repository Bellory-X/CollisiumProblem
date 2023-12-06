using ColiseumLibrary.Contracts.Cards;
using Microsoft.AspNetCore.Mvc;
using SecondPlayerApi.Services;

namespace SecondPlayerApi.Controllers;

[ApiController]
[Route("api/second")]
public class HomeController(PlayerService service) : ControllerBase
{
    [HttpPost]
    public async Task<int> GetCardNumber(Card[] cards) => await service.GetCardNumber(cards);
}
