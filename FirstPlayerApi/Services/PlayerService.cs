using ColiseumLibrary.Contracts.Cards;
using ColiseumLibrary.Contracts.Strategies;

namespace FirstPlayerApi.Services;

public class PlayerService(ICardPickStrategy strategy)
{
    public async Task<int> GetCardNumber(Card[] cards) => await Task.Run(() => strategy.Pick(cards));
}