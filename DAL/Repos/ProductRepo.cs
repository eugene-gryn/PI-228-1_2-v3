using DAL.EF;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repos;

public class ProductRepo : IRepository<Product>
{
    
    private readonly MainContext _mainContext;
    
    public ProductRepo(MainContext mainContext)
    {
        _mainContext = mainContext;
    }


    public async Task<Product> Create(Product item)
    {
        item.ID = 0;
        await _mainContext.Products.AddAsync(item);

        return item;

    }

    public IQueryable<Product> Read()
    {
        return _mainContext.Products.AsQueryable();
    }
    

    public async Task<bool> Update(Product item)
    {
        _mainContext.Entry(item).State = EntityState.Modified; //TODO may be wrong, check!
        return true;
    }

    public async Task<bool> Delete(int id)
    {
        var p = await _mainContext.Products.FindAsync(id);
        if (p == null) return false;
        
        _mainContext.Products.Remove(p);
        return true;
    }
}