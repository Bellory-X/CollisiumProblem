using System.Collections.Immutable;
using ColiseumLibrary.Contracts.Cards;

namespace ColiseumLibrary.Contracts.Orders;

public class Order
{
    public int Id { get; set; }
    public ImmutableArray<Card> Cards { get; set; }
}