namespace Tyl.LondonStockExchange.Core.Interfaces;

public interface IBaseRepository<T> where T : class
{
    T Add(T data);
    
    List<T> Get(string reference);
}