using System.Collections.Immutable;
using ColiseumLibrary.Contracts.Cards;
using Microsoft.AspNetCore.Mvc;
using FirstPlayerApi.Services;

namespace FirstPlayerApi.Controllers;

[ApiController]
[Route("api/first")]
public class HomeController(PlayerService service) : ControllerBase
{
    [HttpPost]
    public async Task<int> GetCardNumber(ImmutableArray<Card> request) => await service.GetCardNumber(request);
}
