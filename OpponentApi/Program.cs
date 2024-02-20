using ColiseumLibrary.Interfaces;
using ColiseumLibrary.Services;
using ColiseumLibrary.Strategies;
using MassTransit;
using OpponentApi.Consumers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<PlayerService>();
builder.Services.AddSingleton<ICardPickStrategy, LastCardStrategy>();
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<OpponentConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.ReceiveEndpoint("opponent", e => 
        { 
            e.ConfigureConsumer<OpponentConsumer>(context);
        });
    });
});

var app = builder.Build();

app.MapControllers();
app.Run();