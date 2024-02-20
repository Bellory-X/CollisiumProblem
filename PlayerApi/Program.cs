using ColiseumLibrary.Interfaces;
using ColiseumLibrary.Services;
using ColiseumLibrary.Strategies;
using MassTransit;
using PlayerApi.Consumers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<PlayerService>();
builder.Services.AddSingleton<ICardPickStrategy, FirstCardStrategy>();
builder.Services.AddMassTransit(x =>
{
    
    x.AddConsumer<PlayerConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(new Uri("rabbitmq://localhost"), h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
        cfg.ReceiveEndpoint("player", e =>
            {
                // e.ConfigureConsumeTopology = false;
                e.Consumer<PlayerConsumer>();
                    // <PlayerConsumer>(context);
            });
    });
});

var app = builder.Build();

app.MapControllers();
app.Run();