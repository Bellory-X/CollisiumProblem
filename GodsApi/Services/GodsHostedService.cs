using ColiseumLibrary.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace GodsApi.Services;

public class GodsHostedService(
    ILogger<GodsHostedService> logger,
    IHostApplicationLifetime appLifetime,
    IExperimentService service 
    ) : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        appLifetime.ApplicationStarted.Register(() =>
        {
            try
            {
                while (true)
                {
                    service.Run();
                }
            }
            catch (Exception e)
            {
                logger.LogError(e, "Unhandled exception!");
            }
            finally
            {
                appLifetime.StopApplication();
            }
        });
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}