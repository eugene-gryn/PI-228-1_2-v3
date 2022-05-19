namespace DAL.Repos;

public interface IRepository<T> where T:class
{
    void Create(T item);
    IEnumerable<T> GetAll();
    T Get(int id);
    IEnumerable<T> Find(Func<T, bool> predicate);
    void Update(T item);
    void Delete(int id);
}