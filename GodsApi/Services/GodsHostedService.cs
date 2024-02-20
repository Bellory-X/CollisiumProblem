using ColiseumLibrary.Interfaces;
using Microsoft.Extensions.Hosting;

namespace GodsApi.Services;

public class GodsHostedService(IExperimentService service) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        while (true)
        {
            var id = Convert.ToInt32(Console.ReadLine());
            if (id < 1) return;
            await service.Run(id);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}