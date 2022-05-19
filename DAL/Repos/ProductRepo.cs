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


    public void Create(Product item)
    {
        item.ID = 0;
        /* ????
         * item.Price = 0f; 
          item.RemainingStock = 0; */
    }

    public IQueryable<Product> Read()
    {
        return _mainContext.Products.AsQueryable();
    }
    

    public void Update(Product item)
    {
        _mainContext.Entry(item).State = EntityState.Modified;
    }

    public void Delete(int id)
    {
        var p = _mainContext.Products.Find(id);
        if(p != null)
            _mainContext.Products.Remove(p);
    }
}