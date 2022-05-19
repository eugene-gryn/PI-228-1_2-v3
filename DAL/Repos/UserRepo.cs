using DAL.Entities;

namespace DAL.Repos;

public class UserRepo : IRepository<User>
{
    public void Create(User item)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<User> GetAll()
    {
        throw new NotImplementedException();
    }

    public User Get(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<User> Find(Func<User, bool> predicate)
    {
        throw new NotImplementedException();
    }

    public void Update(User item)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }
}