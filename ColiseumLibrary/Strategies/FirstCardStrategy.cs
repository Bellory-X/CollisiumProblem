using ColiseumLibrary.Interfaces;
using ColiseumLibrary.Model.Cards;

namespace ColiseumLibrary.Strategies;

/// <summary>
/// Стратегия выбора первой карты из колоды
/// </summary>
public class FirstCardStrategy : ICardPickStrategy
{
    public int Pick(Card[] cards) => 0;
}