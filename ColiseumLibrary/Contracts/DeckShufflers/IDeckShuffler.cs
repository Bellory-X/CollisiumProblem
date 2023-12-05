using ColiseumLibrary.Contracts.Cards;

namespace ColiseumLibrary.Contracts.DeckShufflers;

/// <summary>
/// Тасовщик колод
/// </summary>
public interface IDeckShuffler
{
    /// <summary>
    /// Тасует карты в колодах
    /// </summary>
    /// <param name="firstDeck">Первая стопка карт</param>
    /// <param name="secondDeck">Вторая стопка карт</param>
    public void ShuffleDeck(out Card[] firstDeck, out Card[] secondDeck);
}