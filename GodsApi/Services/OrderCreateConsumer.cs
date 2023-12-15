using ColiseumLibrary.Contracts.Orders;
using ColiseumLibrary.Interfaces;
using GodsApi.Repository;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace GodsApi.Services;

public class OrderCreateConsumer(
    ILogger<OrderCreateConsumer> logger, 
    IExperimentRepository repository
    ) : IConsumer<OrderCreated>
{
    private readonly Dictionary<int, int> _playerChoices = new();

    public Task Consume(ConsumeContext<OrderCreated> context)
    {
        var order = context.Message;
        if (!_playerChoices.ContainsKey(order.Id))
        {
            _playerChoices.Add(order.Id, order.CardNumber);
            logger.LogInformation("Add choice: id: {}, {}", order.Id, order.CardNumber);
            return Task.CompletedTask;
        }
        var exp = repository.GetExperimentById(order.Id);
        if (exp is null)
        {
            logger.LogInformation("Experiment {} not found", order.Id);
            return Task.CompletedTask;
        }
        var output = _playerChoices[order.Id] == order.CardNumber;
        if (exp.Output is null)
        {
            logger.LogInformation("Create experiment {} with output = {}", order.Id, output);
            repository.AddExperiment(exp with { Output = output });
            return Task.CompletedTask;
        }
        logger.LogInformation("Update experiment {}, old output = {}, new output {}", 
            order.Id, exp.Output, output);
        return Task.CompletedTask;
    }
}