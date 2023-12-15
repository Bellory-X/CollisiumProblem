using System.Collections.Immutable;
using ColiseumLibrary.Model.Cards;

namespace ColiseumLibrary.Model.Orders;

public class Order
{
    public int Id { get; set; }
    public ImmutableArray<Card> Cards { get; set; }
}