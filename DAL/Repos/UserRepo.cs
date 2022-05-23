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
        item.ID = 0;
        item.IsAdmin = false;
        item.IsModer = false;

        _mainContext.Add(item);
    }

    public IQueryable<User> Read()
    {
        return _mainContext.Users.AsQueryable();
    }

    public void Update(User item)
    {
        _mainContext.Entry(item).State = EntityState.Modified; //TODO may be wrong, check!

    }

    public void Delete(int id)
    {
        var p = _mainContext.Users.Find(id);
        if (p != null)
            _mainContext.Users.Remove(p);
    }
}