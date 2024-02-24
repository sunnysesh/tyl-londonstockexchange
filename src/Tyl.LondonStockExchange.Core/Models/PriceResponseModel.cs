namespace Tyl.LondonStockExchange.Core.Models;

public class PriceResponseModel
{
    public Price[] Prices { get; set; }
}

public class Price
{
    public string Ticker { get; set; }
    public decimal TotalValue { get; set; }
}