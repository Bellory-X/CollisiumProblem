using ColiseumLibrary.Contracts.Cards;
using ColiseumLibrary.Contracts.Strategies;

namespace SecondPlayerApi.Services;

public class PlayerService
{
    private readonly ICardPickStrategy _strategy;

    public PlayerService(ICardPickStrategy strategy)
    {
        _strategy = strategy;
    }
    
    public async Task<int> GetCardNumber(Card[] cards)
    {
        return await Task.Run(() => _strategy.Pick(cards));
    }
}