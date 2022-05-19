using DAL.EF;
using DAL.Entities;

namespace DAL.Repos;

public class OrderRepo : IRepository<Order>
{
    private readonly MainContext _mainContext;
    
    public OrderRepo(MainContext mainContext)
    {
        _mainContext = mainContext;
    }

    public void Create(Order item)
    {
        throw new NotImplementedException();
    }

    public IQueryable<Order> Read()
    {
        throw new NotImplementedException();
    }

    /*public IEnumerable<Order> GetAll()
    {
        throw new NotImplementedException();
    }

    public Order Get(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Order> Find(Func<Order, bool> predicate)
    {
        throw new NotImplementedException();
    }*/

    public void Update(Order item)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }
}