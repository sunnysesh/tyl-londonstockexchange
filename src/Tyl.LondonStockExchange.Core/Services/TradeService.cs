using Tyl.LondonStockExchange.Core.Interfaces;
using Tyl.LondonStockExchange.Core.Models;
using Tyl.LondonStockExchange.Core.Entities;

namespace Tyl.LondonStockExchange.Core.Services;

public class TradeService : ITradeService
{
    private readonly IBaseRepository<Trade> _tradeRepository;
    
    public TradeService(IBaseRepository<Trade> tradeRepository)
    {
        _tradeRepository = tradeRepository;
    }

    public Trade CreateTrade(TradeRequestModel request)
    {
        try
        {
            var trade = new Trade()
            {
                Ticker = request.Ticker,
                Price = request.Price,
                Shares = request.Shares,
                BrokerId = request.BrokerId
            };
            var newTrade = _tradeRepository.Add(trade);
            return newTrade;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception();
        }
    }
}