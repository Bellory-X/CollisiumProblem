// See https://aka.ms/new-console-template for more information

using ConsoleApp1.Nsu.ColiseumProblem.Contracts;
using ConsoleApp1.Nsu.ColiseumProblem.Contracts.Cards;
using Microsoft.Extensions.Hosting;

class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }
    
    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                // services.AddHostedService<CollisiumExperimentWorker>();
                // services.AddScoped<CollisiumSandbox>();
                // services.AddScoped<IDeckShufller, DeckShufller>();
                // Зарегистрировать партнеров и их стратегии
            });
    }
    
    // public static void Main(string[] args)
    // {
    //     
    //     float successCount = 0;
    //     var player1 = new FirstStrategy();
    //     var player2 = new SecondStrategy();
    //     var shuffler = new DeckShuffler();
    //     shuffler.Deck.FirstDeck[0] = new Card(CardColor.Black);
    //     for (var i = 0; i < ExperimentCounts; i++)
    //     {
    //         var firstColor = shuffler.Deck.FirstDeck[player2.Pick(shuffler.Deck.SecondDeck)];
    //         var secondColor = shuffler.Deck.SecondDeck[player1.Pick(shuffler.Deck.FirstDeck)];
    //         shuffler.Stir();
    //         if (secondColor == firstColor)
    //             successCount++;
    //     }
    //     Console.WriteLine(successCount / ExperimentCounts * 100);
    // }
}