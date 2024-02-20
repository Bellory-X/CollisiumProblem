using ColiseumLibrary.Data;
using ColiseumLibrary.DeckShufflers;
using ColiseumLibrary.Interfaces;
using ColiseumLibrary.Repository;
using ColiseumLibrary.Services;
using GodsApi.Consumers;
using GodsApi.Services;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddDbContext<ExperimentDbContext>(x => 
            x.UseSqlite("FileName=applicationDb.db"));
builder.Services.AddHostedService<GodsHostedService>();
builder.Services.AddSingleton<IExperimentRepository, ExperimentRepository>();
builder.Services.AddSingleton<IDeckShuffler, RandomDeckShuffler>();
builder.Services.AddSingleton<IExperimentService, GodsHardExperimentService>();
builder.Services.AddMassTransit(x => 
{
    x.AddConsumer<GodsConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(new Uri("rabbitmq://localhost"), h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
        cfg.ReceiveEndpoint(/*"player", */e => 
        {
            e.Consumer<GodsConsumer>();
            // e.ConfigureConsumer<GodsConsumer>(context);
        });
        // cfg.ReceiveEndpoint("opponent", e => 
        // {
        //     e.ConfigureConsumer<GodsConsumer>(context);
        // });
    });
});

var app = builder.Build();

app.Run();