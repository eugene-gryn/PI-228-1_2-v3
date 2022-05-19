using DAL.EF;
using DAL.Entities;
using DAL.Repos;

namespace DAL.UOW;

public class EFUnitOfWork : IUnitOfWork
{
    
    private readonly MainContext _mainContext;
    private bool _disposed = false;

    private IRepository<User> _users;
    public  IRepository<User> Users => _users ??= new UserRepo(_mainContext);
    
    private IRepository<Product> _products;
    public  IRepository<Product> Products => _products ??= new ProductRepo(_mainContext);
    
    private IRepository<Order> _orders;
    public  IRepository<Order> Orders => _orders ??= new OrderRepo(_mainContext);


    public EFUnitOfWork (MainContext mainContext)
    {
        _mainContext = mainContext;
    }


    public void Save()
    {
        _mainContext.SaveChanges();
    }


    protected virtual void Dispose(bool disposing)
    {
        if (_disposed) return;
        if (disposing)
        {
            _mainContext.Dispose();
        }
        _disposed = true;
    }
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}