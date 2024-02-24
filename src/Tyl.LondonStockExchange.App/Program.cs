using Tyl.LondonStockExchange.Core.Interfaces;
using Tyl.LondonStockExchange.Core.Services;
using Tyl.LondonStockExchange.Core.Entities;
using Tyl.LondonStockExchange.Core.Models;
using Tyl.LondonStockExchange.Core.Validators;
using Tyl.LondonStockExchange.Infrastructure.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IValidator<TradeRequestModel>, TradeValidator>();

builder.Services.AddTransient<ITradeService, TradeService>();
builder.Services.AddTransient<IPriceService, PriceService>();
builder.Services.AddSingleton<IBaseRepository<Trade>, TradeRepository>(); //Removed once db is in place

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/trades", async (IValidator<TradeRequestModel> tradeValidator, ITradeService tradeService, TradeRequestModel request) =>
{
    var validationResult = await tradeValidator.ValidateAsync(request);
    if (!validationResult.IsValid)
        return Results.ValidationProblem(validationResult.ToDictionary());

    var response = tradeService.CreateTrade(request);
    return Results.Created("/trades", response);
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
