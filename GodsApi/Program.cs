using ColiseumLibrary.Contracts.Cards;
using GodsApi.Data;
using GodsApi.Repository;
using GodsApi.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

await Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddAutoMapper(typeof(MappingProfile));
        services.AddHostedService<GodsHostedService>();
        services.AddScoped<IExperimentRepository, ExperimentRepository>();
        services.AddSingleton<Deck>();
        services.AddSingleton<IWorker, ExperimentWorker>();
        services.AddDbContext<ApplicationDbContext>();
    })
    .RunConsoleAsync();