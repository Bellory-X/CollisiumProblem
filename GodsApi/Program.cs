using ColiseumLibrary.Contracts.DeckShufflers;
using GodsApi.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

await Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHostedService<GodsHostedService>();
        services.AddSingleton<IDeckShuffler, SimpleDeckShuffler>();
        services.AddSingleton<IWorker, ExperimentWorker>();
    })
    .RunConsoleAsync();