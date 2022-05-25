using DAL.Entities;

namespace DAL.Repos;

public interface IRepository<T> where T:class
{
    Task<T> Create(T item);
    IQueryable<T> Read();
    Task<bool> Update(T item);
    Task<bool> Delete(int id);
}