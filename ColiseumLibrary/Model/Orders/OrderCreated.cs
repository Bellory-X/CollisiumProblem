namespace ColiseumLibrary.Model.Orders;

public class OrderCreated
{
    public int Id { get; init; }
    public int Ordinal { get; init; }
    public bool IsPlayer { get; init; }
}