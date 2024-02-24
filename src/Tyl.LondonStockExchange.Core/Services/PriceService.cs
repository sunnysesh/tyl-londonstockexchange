using Tyl.LondonStockExchange.Core.Models;
using Tyl.LondonStockExchange.Core.Interfaces;

namespace Tyl.LondonStockExchange.Core.Services;

public class PriceService : IPriceService
{
    public PriceService()
    {
        
    }

    public async Task<List<PriceResponseModel>> GetPricesViaTicker(string[] tickers)
    {
        
    }

    public async Task<List<PriceResponseModel>> GetAllPrices()
    {
        
    }
}