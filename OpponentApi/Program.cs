using ColiseumLibrary.Interfaces;
using ColiseumLibrary.Strategies;
using ColiseumLibrary.Workers;
using MassTransit;
using OpponentApi.Consumers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<OrderService>();
builder.Services.AddSingleton<ICardPickStrategy, LastCardStrategy>();
// builder.Services.AddMassTransit(x =>
// {
//     x.AddConsumer(typeof(OrderConsumer));
//     x.UsingRabbitMq((context, cfg) =>
//     {
//         cfg.ReceiveEndpoint("opponent",
//             e =>
//             {
//                 e.ConfigureConsumer<OrderConsumer>(context);
//             });
//     });
// });

var app = builder.Build();

app.MapControllers();
app.Run();