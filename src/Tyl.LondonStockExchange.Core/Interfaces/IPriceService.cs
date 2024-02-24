using Tyl.LondonStockExchange.Core.Models;

namespace Tyl.LondonStockExchange.Core.Interfaces;

public interface IPriceService
{
    List<PriceResponseModel> GetPricesViaTicker(string[] tickers);

    List<PriceResponseModel> GetAllPrices();
}