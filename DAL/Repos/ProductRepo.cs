namespace DAL.Repos;

public class ProductRepo : IRepository<ProductRepo>
{
    public void Create(ProductRepo item)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<ProductRepo> GetAll()
    {
        throw new NotImplementedException();
    }

    public ProductRepo Get(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<ProductRepo> Find(Func<ProductRepo, bool> predicate)
    {
        throw new NotImplementedException();
    }

    public void Update(ProductRepo item)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }
}