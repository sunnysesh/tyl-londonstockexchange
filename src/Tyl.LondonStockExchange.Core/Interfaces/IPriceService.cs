using Tyl.LondonStockExchange.Core.Models;

namespace Tyl.LondonStockExchange.Core.Interfaces;

public interface IPriceService
{
    Task<List<PriceResponseModel>> GetPricesViaTicker(string[] tickers);

    Task<List<PriceResponseModel>> GetAllPrices();
}