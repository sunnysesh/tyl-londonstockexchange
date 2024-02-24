using Tyl.LondonStockExchange.Core.Interfaces;
using Tyl.LondonStockExchange.Core.Entities;

namespace Tyl.LondonStockExchange.Infrastructure.Repositories;

public class TradeRepository : IBaseRepository<Trade>
{
    public List<Trade> Transactions = new List<Trade>(); //Should be replaced with db
    
    public TradeRepository()
    {
    }

    public Trade Add(Trade trade)
    {
        Transactions.Add(trade);
        return Transactions.Where(i => i.Id == trade.Id).FirstOrDefault();
    }

    public List<Trade> Get(string reference)
    {
        var results = Transactions.Where(i => i.Ticker.Equals(reference)).ToList();
        return results;
    }
}