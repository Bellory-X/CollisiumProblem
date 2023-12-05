using ColiseumLibrary.Contracts.Cards;
using Microsoft.AspNetCore.Mvc;
using SecondPlayerApi.Services;

namespace SecondPlayerApi.Controllers;

[ApiController]
[Route("api/second")]
public class HomeController : ControllerBase
{
    private readonly PlayerService _service;
    
    public HomeController(PlayerService service)
    {
        _service = service;
    }
    
    [HttpPost]
    public async Task<int> GetCardNumber(Card[] cards)
    {
        return await _service.GetCardNumber(cards);
    }
}
