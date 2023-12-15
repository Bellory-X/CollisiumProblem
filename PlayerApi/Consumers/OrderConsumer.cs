using ColiseumLibrary.Model.Orders;
using ColiseumLibrary.Workers;
using MassTransit;

namespace PlayerApi.Consumers;

public class OrderConsumer(OrderService service) : IConsumer<Order>
{
    public Task Consume(ConsumeContext<Order> context) => context.Publish(service.CreateOrder(context.Message));
}