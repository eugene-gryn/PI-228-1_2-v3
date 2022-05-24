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

    public async Task<Order> Create(Order item)
    {
        item.ID = 0;
        item.Processed = false;

        await _mainContext.AddAsync(item);

        return item;
    }

    public IQueryable<Order> Read()
    {
        return _mainContext.Orders.AsQueryable();
    }
    

    public async Task<bool> Update(Order item)
    {
        _mainContext.Entry(item).State = EntityState.Modified; //TODO may be wrong, check!
        return true;//TODO add checks?
    }

    public async Task<bool> Delete(int id)
    {
        var o = await _mainContext.Orders.Include(order =>order.ProductAmounts).FirstOrDefaultAsync();
        if (o == null) return false;
        
        _mainContext.Orders.Remove(o);

        return true;
    }
}