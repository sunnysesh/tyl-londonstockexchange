using Tyl.LondonStockExchange.Core.Models;

namespace Tyl.LondonStockExchange.Core.Interfaces;

public interface ITradeService
{
    Task CreateTrade(TradeRequestModel request);
}