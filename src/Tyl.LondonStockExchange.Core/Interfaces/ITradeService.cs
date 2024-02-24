using Tyl.LondonStockExchange.Core.Models;
using Tyl.LondonStockExchange.Core.Entities;

namespace Tyl.LondonStockExchange.Core.Interfaces;

public interface ITradeService
{
    Trade CreateTrade(TradeRequestModel request);
}