using DAL.EF;
using DAL.Entities;

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
        throw new NotImplementedException();
    }

    public IQueryable<Product> Read()
    {
        throw new NotImplementedException();
    }

    public void Update(Product item)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }
}