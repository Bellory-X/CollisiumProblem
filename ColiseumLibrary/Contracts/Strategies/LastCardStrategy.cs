using ColiseumLibrary.Contracts.Cards;
using ColiseumLibrary.Interfaces;

namespace ColiseumLibrary.Contracts.Strategies;

public class LastCardStrategy : ICardPickStrategy
{
    public int Pick(Card[] cards) => cards.Length - 1;
}