using ColiseumLibrary.DeckShufflers;
using ColiseumLibrary.Interfaces;
using ColiseumLibrary.Workers;
using GodsApi.Data;
using GodsApi.Repository;
using GodsApi.Services;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

await Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        // services.AddAutoMapper(typeof(MappingProfiles));
        services.AddDbContext<ExperimentDbContext>(x => 
            x.UseSqlite("FileName=applicationDb.db"));
        services.AddHostedService<GodsHostedService>();
        services.AddSingleton<IExperimentRepository, ExperimentRepository>();
        services.AddSingleton<IDeckShuffler, RandomDeckShuffler>();
        services.AddSingleton<IExperimentService, ExperimentWebService>();
        // services.AddSingleton<IExperimentService, ExperimentMassTransitService>();
        // services.AddMassTransit(x =>
        // {
        //     x.AddConsumer(typeof(OrderCreateConsumer));
        //     x.UsingRabbitMq();
        // });
    })
    .RunConsoleAsync();