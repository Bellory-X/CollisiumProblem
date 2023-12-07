using ColiseumLibrary.Contracts.Strategies;
using SecondPlayerApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<PlayerService>();
builder.Services.AddSingleton<ICardPickStrategy, LastCardStrategy>();

var app = builder.Build();

app.MapControllers();
app.Run();