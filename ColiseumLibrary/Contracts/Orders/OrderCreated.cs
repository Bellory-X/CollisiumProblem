using ColiseumLibrary.Interfaces;

namespace ColiseumLibrary.Contracts.Orders;

public class OrderCreated
{
    public int Id { get; set; }
    public int CardNumber { get; set; }
}