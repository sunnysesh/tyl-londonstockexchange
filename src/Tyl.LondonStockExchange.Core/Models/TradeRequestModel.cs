namespace Tyl.LondonStockExchange.Core.Models;

public class TradeRequestModel
{
    public string Ticker { get; set; }
    public decimal Price { get; set; }
    public decimal Shares { get; set; }
    public string BrokerId { get; set; }
}