using Tyl.LondonStockExchange.Core.Models;

namespace Tyl.LondonStockExchange.Core.Entities;

public class Trade : TradeRequestModel
{
    public Guid Id { get; set; }
}