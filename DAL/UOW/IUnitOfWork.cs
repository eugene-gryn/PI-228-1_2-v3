using DAL.Entities;
using DAL.Repos;

namespace DAL.UOW;

public interface IUnitOfWork : IDisposable
{
    IRepository<User> Users { get;}
    IRepository<Product> Products { get;}
    IRepository<Order> Orders { get;}

    void Save();
}