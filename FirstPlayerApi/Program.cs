using ColiseumLibrary.Contracts.Strategies;
using FirstPlayerApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<PlayerService>();
builder.Services.AddSingleton<ICardPickStrategy, FirstCardStrategy>();

var app = builder.Build();

app.MapControllers();
app.Run();