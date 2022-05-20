using DAL.EF;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

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
        item.ID = 0;
        item.Processed = false; 

        _mainContext.Add(item);
    }

    public IQueryable<Order> Read()
    {
        return _mainContext.Orders.AsQueryable();
    }
    

    public void Update(Order item)
    {
        _mainContext.Entry(item).State = EntityState.Modified; //TODO may be wrong, check!
    }

    public void Delete(int id)
    {
        var o = _mainContext.Orders.Find(id);
        if(o != null)
            _mainContext.Orders.Remove(o);
    }
}