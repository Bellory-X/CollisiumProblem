using ColiseumLibrary.Contracts.Cards;
using Microsoft.AspNetCore.Mvc;
using FirstPlayerApi.Services;

namespace FirstPlayerApi.Controllers;

[ApiController]
[Route("api/first")]
public class HomeController : ControllerBase
{
    private readonly PlayerService _service;

    public HomeController(PlayerService service)
    {
        _service = service;
    }
    
    [HttpPost]
    public async Task<int> GetCardNumber(Card[] request)
    {
        return await _service.GetCardNumber(request);
    }
}
