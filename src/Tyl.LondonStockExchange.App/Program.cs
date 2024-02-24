using Tyl.LondonStockExchange.Core.Interfaces;
using Tyl.LondonStockExchange.Core.Services;
using Tyl.LondonStockExchange.Core.Entities;
using Tyl.LondonStockExchange.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<ITradeService, TradeService>();
builder.Services.AddSingleton<IBaseRepository<Trade>, TradeRepository>(); //Removed once db is in place

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/trades", () =>
{
});

app.MapGet("/price/{ticker}", () =>
{
});

app.MapGet("/prices", () =>
{
});

app.MapGet("/prices/tickers", () =>
{
});

app.Run();
