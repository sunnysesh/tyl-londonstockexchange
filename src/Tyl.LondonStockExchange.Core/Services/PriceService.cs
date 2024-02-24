using Tyl.LondonStockExchange.Core.Models;
using Tyl.LondonStockExchange.Core.Interfaces;
using Tyl.LondonStockExchange.Core.Entities;

namespace Tyl.LondonStockExchange.Core.Services;

public class PriceService : IPriceService
{
    private readonly IBaseRepository<Trade> _tradeRepository;
    
    public PriceService(IBaseRepository<Trade> tradeRepository)
    {
        _tradeRepository = tradeRepository;
    }

    public List<PriceResponseModel> GetPrices(string[] tickers = null)
    {
        var transactions = tickers?.Length > 0
            ? _tradeRepository.Get(tickers)
            : _tradeRepository.Get();
        
        if (!transactions.Any())
            return null;
        
        var groupedTransactions = ProcessTransactions(transactions);
        var priceResponse = MapToPriceResponse(groupedTransactions);
        return priceResponse;
    }

    private Dictionary<string, decimal>? ProcessTransactions(List<Trade> transactions)
        => transactions?.GroupBy(i => i.Ticker)
            .ToDictionary(group => group.Key, group => group.Sum(transaction => transaction.Price));

    private List<PriceResponseModel>? MapToPriceResponse(Dictionary<string, decimal> groupedTransactions)
        => groupedTransactions?.Select(i => new PriceResponseModel
            {
                Ticker = i.Key,
                TotalValue = i.Value
            })
            .ToList();
}