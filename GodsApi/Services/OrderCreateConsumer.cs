using ColiseumLibrary.Interfaces;
using ColiseumLibrary.Model.Experiments;
using ColiseumLibrary.Model.Orders;
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
        if (!_playerChoices.ContainsKey(order.Id)) return AddChoice(order);
        
        var exp = repository.GetExperimentById(order.Id);
        if (exp is not null) return WorkExperiment(order, exp);
        
        logger.LogInformation("Experiment {} not found", order.Id);
        
        return Task.CompletedTask;
    }

    private Task AddChoice(OrderCreated order)
    {
        _playerChoices.Add(order.Id, order.CardNumber);
        logger.LogInformation("Add choice: id: {}, value: {}", order.Id, order.CardNumber);
        return Task.CompletedTask;
    }

    private Task WorkExperiment(OrderCreated order, Experiment experiment)
    {
        var output = _playerChoices[order.Id] == order.CardNumber;
        
        if (experiment.Output is null) 
            logger.LogInformation("Create experiment {} with output = {}", order.Id, output);
        else 
            logger.LogInformation("Update experiment {}, old output = {}, new output {}", 
                order.Id, experiment.Output, output);
        
        repository.AddExperiment(experiment with { Output = output });
        
        return Task.CompletedTask;
    }
}