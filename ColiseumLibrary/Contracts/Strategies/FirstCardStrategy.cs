using ColiseumLibrary.Contracts.Cards;

namespace ColiseumLibrary.Contracts.Strategies;

/// <summary>
/// Стратегия выбора первой карты из колоды
/// </summary>
public class FirstCardStrategy : ICardPickStrategy
{
    public int Pick(Card[] cards) => 0;
}