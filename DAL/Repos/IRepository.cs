using DAL.Entities;

namespace DAL.Repos;

public interface IRepository<T> where T:class
{
    void Create(T item);
    IQueryable<T> Read();
    void Update(T item);
    void Delete(int id);
}