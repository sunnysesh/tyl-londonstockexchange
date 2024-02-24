using Tyl.LondonStockExchange.Core.Models;

namespace Tyl.LondonStockExchange.Core.Interfaces;

public interface IPriceService
{
    List<PriceResponseModel> GetPrices(string[] tickers = null);
}