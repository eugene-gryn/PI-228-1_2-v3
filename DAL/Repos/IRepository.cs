using DAL.Entities;

namespace DAL.Repos;

public interface IRepository<T> where T:class
{
    void Create(T item);
    /*IEnumerable<T> GetAll();
    IQueryable<User> Get(int id);
    IEnumerable<T> Find(Func<T, bool> predicate);*/
    IQueryable<T> Read();
    void Update(T item);
    void Delete(int id);
}