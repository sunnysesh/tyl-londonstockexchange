using Tyl.LondonStockExchange.Core.Models;

namespace Tyl.LondonStockExchange.Infrastructure.Entities;

public class Trade : TradeRequestModel
{
    public Guid Id { get; set; }
}