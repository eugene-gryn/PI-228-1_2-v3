using DAL.EF;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repos;

public class UserRepo : IRepository<User>
{
    private readonly MainContext _mainContext;
    
    public UserRepo(MainContext mainContext)
    {
        _mainContext = mainContext;
    }

    public void Create(User item)
    {
        _mainContext.Add(item);
    }

    public IQueryable<User> Read()
    {
        return _mainContext.Users.AsQueryable();
    }

    /*public IEnumerable<User> GetAll()
    {
        return _mainContext.Users;
    }

    public IQueryable<User> Ку(int id)
    {
        return _mainContext.Users.AsQueryable();
    }

    public IEnumerable<User> Find(Func<User, bool> predicate)
    {
        return _mainContext.Users.Where(predicate).ToList();
    }*/

    public void Update(User item)
    {
        _mainContext.Entry(item).State = EntityState.Modified; //TODO may be wrong
    }

    public void Delete(int id)
    {
        var p = _mainContext.Users.Find(id);
        if (p != null)
            _mainContext.Users.Remove(p);
    }
}