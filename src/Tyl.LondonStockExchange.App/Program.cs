var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
