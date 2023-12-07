using System.Collections.Immutable;
using ColiseumLibrary.Contracts.Cards;
using ColiseumLibrary.Contracts.Strategies;

namespace SecondPlayerApi.Services;

public class PlayerService(ICardPickStrategy strategy)
{
    public async Task<int> GetCardNumber(ImmutableArray<Card> cards) => 
        await Task.Run(() => strategy.Pick(cards.ToArray()));
}