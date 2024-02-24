using Tyl.LondonStockExchange.Core.Interfaces;
using Tyl.LondonStockExchange.Core.Entities;
using System.Linq;

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

    public List<Trade> Get(string[] references = null)
    {
        if (references == null || !references.Any())
            return Transactions;
            
        return Transactions.Where(i => references.Contains(i.Ticker)).ToList();
    }
}