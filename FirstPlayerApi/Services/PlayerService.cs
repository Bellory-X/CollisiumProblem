using ColiseumLibrary.Contracts.Cards;
using ColiseumLibrary.Contracts.Strategies;

namespace FirstPlayerApi.Services;

public class PlayerService
{
    private readonly ILogger<PlayerService> _logger;
    private readonly ICardPickStrategy _strategy;

    public PlayerService(ILogger<PlayerService> logger, ICardPickStrategy strategy)
    {
        _logger = logger;
        _strategy = strategy;
    }
    
    public async Task<int> GetCardNumber(Card[] cards)
    {
        return await Task.Run(() => 
        {
            try
            {
                return _strategy.Pick(cards);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Unhandled exception!");
                throw;
            }
        });
    }
}