﻿namespace ColiseumLibrary.Model.Cards;

/// <summary>
/// Игральная карта
/// </summary>
public record Card(CardColor Color)
{
    public override string ToString() => Color == CardColor.Black ? "♠️" : "♦️";
}