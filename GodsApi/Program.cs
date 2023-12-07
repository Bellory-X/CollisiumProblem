using ColiseumLibrary.Contracts.DeckShufflers;
using GodsApi.Data;
using GodsApi.Repository;
using GodsApi.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

await Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddAutoMapper(typeof(MappingProfiles));
        services.AddHostedService<HostedService>();
        services.AddSingleton<IExperimentRepository, ExperimentRepository>();
        services.AddSingleton<IDeckShuffler, RandomDeckShuffler>();
        services.AddSingleton<IWorker, ExperimentWorker>();
        services.AddDbContext<ApplicationDbContext>();
    })
    .RunConsoleAsync();